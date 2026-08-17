using ControleAnaliseDesembolso.Application.Dtos.Request;
using ControleAnaliseDesembolso.Application.Dtos.Response;
using ControleAnaliseDesembolso.Application.Interface;
using ControleAnaliseDesembolso.Domain.Entitys;
using ControleAnaliseDesembolso.Domain.Enums;
using ControleAnaliseDesembolso.Hubs;
using ControleAnaliseDesembolso.Infra.Data.Context;
using ControleAnaliseDesembolso.Interface;
using Microsoft.EntityFrameworkCore;
using PlataformaNotificacao.Application.Interface;
using PlataformaNotificacao.Domain.Enum;

namespace ControleAnaliseDesembolso.Application
{
    public class ControleAnaliseDesembolsoService : IControleAnaliseDesembolso
    {
        private readonly ControleAnaliseDesembolsoContext _context;
        private readonly IValidadorDesembolsoService _validador;
        private readonly IEmpregadoCADService _empregados;
        private readonly INotificacaoService _notificacoes;

        // Código real da coordenação CEFGA junto ao PlataformaNotificacao —
        // casa com Empregado.CodigoCoordenacao lá (mock ajustado pra todos
        // pertencerem à "06").
        private const string CodigoCoordenacaoCefga = "06";

        public ControleAnaliseDesembolsoService(
            ControleAnaliseDesembolsoContext context,
            IValidadorDesembolsoService validador,
            IEmpregadoCADService empregados,
            INotificacaoService notificacoes,
            SignalRNotificacaoService signalR)
        {
            _context = context;
            _validador = validador;
            _empregados = empregados;
            _notificacoes = notificacoes;

            // Notificacoes é Scoped (uma instância nova por request, igual este
            // serviço) — então inscrever aqui no construtor é seguro: o handler
            // vive exatamente enquanto essa instância viver, sem vazar entre
            // requisições. Fire-and-forget porque EventHandler<T> é void, não Task.
            notificacoes.OnNotificacao += (sender, e) => _ = signalR.HandlerObserver(sender, e);
        }

        // "contratoId" (não "desembolso") pra bater com o [SupplyParameterFromQuery]
        // já existente em PaginaAnalise.razor, que auto-abre o dialog com esse nome.
        private static string LinkDesembolso(int coDesembolso) => $"/cad?contratoId={coDesembolso}";

        public async Task<string?> ObterREsponsavelDesembolso(int coFpd, CancellationToken cancellationToken = default)
        {
            var fpd = await _context.FichaPedidoDesembolsos
                .Include(x => x.Desembolso)
                .FirstOrDefaultAsync(x => x.CoFpd == coFpd, cancellationToken);

            var response = fpd.Desembolso.ResponsavelAnalise;
            return response;
        }

        // ── Busca o desembolso e devolve o histórico completo de comentários
        // (TB006), de todas as validações, em ordem cronológica. Sem isso, o
        // histórico só existe no estado local do front — some ao recarregar
        // a página, porque nunca existiu um caminho de leitura de volta pro
        // banco (só gravava, nunca relia).
        public async Task<List<ComentarioValidacaoResponse>> ObterComentarios(int coDesembolso, CancellationToken cancellationToken = default)
        {
            var existe = await _context.Desembolsos.AnyAsync(x => x.CoDesembolso == coDesembolso, cancellationToken);
            if (!existe)
                throw new Exception($"Desembolso {coDesembolso} não encontrado.");

            return await _context.RegistroValidacoes
                .Where(x => x.CoDesembolso == coDesembolso && x.Ativo)
                .OrderBy(x => x.DtCriacao)
                .Select(x => new ComentarioValidacaoResponse
                {
                    CoRegistroValidacao = x.CoRegistroValidacao,
                    CoValidacao = x.CoValidacao,
                    Texto = x.DeRegistro,
                    TipoRegistro = x.TipoRegistro,
                    MatriculaAutor = x.CoUsuario,
                    NomeAutor = x.DeUsuario,
                    UnidadeAutor = x.UnidadeUsuario,
                    Sigla = x.UnidadeUsuario == 7175 ? "CEFGA" : "GIGOV",
                    DtCriacao = x.DtCriacao,
                })
                .ToListAsync(cancellationToken);
        }

        // ── Junta ficha + checklist (com status) + comentários de cada item
        // numa resposta só — é o que o dialog de detalhes da Home precisa
        // pra abrir sem ficar disparando uma chamada por seção. Não existe FK
        // entre RegistroValidacao e ValidacaoDesembolso no banco (é só um
        // filtro por CoDesembolso/CoValidacao, igual ObterComentarios já
        // fazia), então o agrupamento por validação é feito em memória.
        public async Task<DesembolsoDetalheResponse> ObterDetalheDesembolso(int coDesembolso, CancellationToken cancellationToken = default)
        {
            var desembolso = await _context.Desembolsos
                .Include(x => x.FichaPedidoDesembolso)
                .Include(x => x.ValidacoesDesembolso)
                    .ThenInclude(x => x.CadastroValidacao)
                .FirstOrDefaultAsync(x => x.CoDesembolso == coDesembolso, cancellationToken);

            if (desembolso is null)
                throw new Exception($"Desembolso {coDesembolso} não encontrado.");

            var comentarios = await _context.RegistroValidacoes
                .Where(x => x.CoDesembolso == coDesembolso && x.Ativo)
                .OrderBy(x => x.DtCriacao)
                .Select(x => new ComentarioValidacaoResponse
                {
                    CoRegistroValidacao = x.CoRegistroValidacao,
                    CoValidacao = x.CoValidacao,
                    Texto = x.DeRegistro,
                    TipoRegistro = x.TipoRegistro,
                    MatriculaAutor = x.CoUsuario,
                    NomeAutor = x.DeUsuario,
                    UnidadeAutor = x.UnidadeUsuario,
                    Sigla = x.UnidadeUsuario == 7175 ? "CEFGA" : "GIGOV",
                    DtCriacao = x.DtCriacao,
                })
                .ToListAsync(cancellationToken);

            var comentariosPorValidacao = comentarios
                .GroupBy(c => c.CoValidacao)
                .ToDictionary(g => g.Key, g => g.ToList());

            var coValidacoesDoChecklist = desembolso.ValidacoesDesembolso
                .Select(v => v.CoValidacao)
                .ToHashSet();

            var fpd = desembolso.FichaPedidoDesembolso;

            return new DesembolsoDetalheResponse
            {
                CoDesembolso = desembolso.CoDesembolso,
                CoFpd = desembolso.CoFpd,
                Status = desembolso.StatusDesembolso,
                DtSolicitado = fpd.DtSolicitado,
                DtPrazo = desembolso.DtPrazo,
                DtConclusao = desembolso.DtConclusao,
                ResponsavelAnalise = desembolso.ResponsavelAnalise,
                ResponsavelBaixa = desembolso.ResponsavelBaixa,
                CoContratoAf = fpd.CoContratoAf,
                CoContratoAfDv = fpd.CoContratoAfDv,
                CoGigov = fpd.CoGigov,
                MutuarioFinal = fpd.MutuarioFinal,
                CnpjMutuarioFinal = fpd.CnpjMutuarioFinal,
                AgenteFinanceiro = fpd.AgenteFinanceiro,
                AgentePromotor = fpd.AgentePromotor,
                Programa = fpd.Programa.ToString(),
                TipoDesembolso = fpd.TipoDesembolso.ToString(),
                PrimeiroDesembolso = fpd.PrimeiroDesembolso,
                UltimoDesembolso = fpd.UltimoDesembolso,
                PercentualObra = fpd.PercentualObra,
                ValorEmprestimo = fpd.ValorEmprestimo,
                SolicitadoVi = fpd.SolicitadoVi,
                ParticipacaoFgts = fpd.ParticipacaoFgts,
                Contrapartida = fpd.Contrapartida,
                MatriculaSolicitante = fpd.MatriculaSolicitante,
                MatriculaGestor = fpd.MatriculaGestor,
                CnpjAf = fpd.CnpjAf,
                AgenteTecnicoOperador = fpd.AgenteTecnicoOperador,
                CnpjAgenteTecnicoOperador = fpd.CnpjAgenteTecnicoOperador,
                CnpjAgentePromotor = fpd.CnpjAgentePromotor,
                DtEngenharia = fpd.DtEngenharia,
                SituacaoObra = fpd.SituacaoObra?.ToString(),
                DtSocioAmbiental = fpd.DtSocioAmbiental,
                Concluido = fpd.Concluido,
                GlossadoVi = fpd.GlossadoVi,
                AceitoVi = fpd.AceitoVi,
                Desembolsado = fpd.Desembolsado,
                SaldoADesembolsar = fpd.SaldoADesembolsar,
                Excepcionalizado = fpd.Excepcionalizado,
                ContrapartidaAtual = fpd.ContrapartidaAtual,
                Integralizado = fpd.Integralizado,
                SaldoAIntegralizar = fpd.SaldoAIntegralizar,
                ContrapartidaAlterada = fpd.ContrapartidaAlterada,
                Amortizacao = fpd.Amortizacao,
                Sanepar = fpd.Sanepar,
                RetornoParcial = fpd.RetornoParcial,
                PlacaLocal = fpd.PlacaLocal,
                LicensaInstalacao = fpd.LicensaInstalacao,
                LicensaOperacao = fpd.LicensaOperacao,
                Funcionalidade = fpd.Funcionalidade,
                Checklist = desembolso.ValidacoesDesembolso
                    .OrderBy(v => v.CoValidacao)
                    .Select(v => new ChecklistItemResponse
                    {
                        CoValidacao = v.CoValidacao,
                        DeValidacao = v.CadastroValidacao?.DeValidacao ?? v.DeValidacao,
                        Situacao = v.Situacao,
                        Comentarios = comentariosPorValidacao.TryGetValue(v.CoValidacao, out var lista) ? lista : new(),
                    })
                    .ToList(),
                ComentariosGerais = comentarios
                    .Where(c => !coValidacoesDoChecklist.Contains(c.CoValidacao))
                    .ToList(),
            };
        }

        public async Task SalvarFichaPedidoDesembolso(PedidoDesembolsoRequest request)
        {
            //_context.FichaPedidoDesembolsos
        }

        // Monta uma FichaPedidoDesembolso nova a partir do request — mesmo
        // conjunto de campos usado em AtualizarDadosFicha (reenvio), só que
        // construindo o objeto do zero em vez de atualizar um existente.
        private static FichaPedidoDesembolso MapearParaFicha(PedidoDesembolsoRequest request)
        {
            return new FichaPedidoDesembolso
            {
                MatriculaSolicitante = request.MatriculaSolicitante,
                CoGigov = request.CoGigov,
                MatriculaGestor = request.MatriculaGestor,
                CoContratoAf = request.CoContratoAf,
                CoContratoAfDv = request.CoContratoAfDv,
                PrimeiroDesembolso = request.PrimeiroDesembolso,
                AgenteFinanceiro = request.AgenteFinanceiro,
                CnpjAf = request.CnpjAf,
                MutuarioFinal = request.MutuarioFinal,
                CnpjMutuarioFinal = request.CnpjMutuarioFinal,
                AgenteTecnicoOperador = request.AgenteTecnicoOperador,
                CnpjAgenteTecnicoOperador = request.CnpjAgenteTecnicoOperador,
                AgentePromotor = request.AgentePromotor,
                CnpjAgentePromotor = request.CnpjAgentePromotor,
                Programa = (Programa)request.Programa,
                UltimoDesembolso = request.UltimoDesembolso,
                Funcionalidade = request.Funcionalidade,
                Concluido = request.Concluido,
                DtEngenharia = request.DtEngenharia,
                SituacaoObra = (TipoSituacaoObra)request.SituacaoObra,
                DtSocioAmbiental = request.DtSocioAmbiental,
                PercentualObra = request.PercentualObra,
                TipoDesembolso = (TipoDesembolso)request.TipoDesembolso,
                RetornoParcial = request.RetornoParcial,
                PlacaLocal = request.PlacaLocal,
                LicensaInstalacao = request.LicensaInstalacao,
                LicensaOperacao = request.LicensaOperacao,
                SolicitadoVi = request.SolicitadoVi,
                GlossadoVi = request.GlossadoVi,
                AceitoVi = request.AceitoVi,
                ParticipacaoFgts = request.ParticipacaoFgts,
                Contrapartida = request.Contrapartida,
                ValorEmprestimo = request.ValorEmprestimo,
                Desembolsado = request.Desembolsado,
                SaldoADesembolsar = request.SaldoADesembolsar,
                Excepcionalizado = request.Excepcionalizado,
                ContrapartidaAtual = request.ContrapartidaAtual,
                Integralizado = request.Integralizado,
                SaldoAIntegralizar = request.SaldoAIntegralizar,
                ContrapartidaAlterada = request.ContrapartidaAlterada,
                Amortizacao = request.Amortizacao,
                Sanepar = request.Sanepar,
            };
        }

        public async Task CriarFichaPedidoDesembolso(PedidoDesembolsoRequest request, CancellationToken cancellationToken = default)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);

            int coDesembolso;

            try
            {
                var fpd = MapearParaFicha(request);
                fpd.DtSolicitado = DateTime.UtcNow;
                fpd.Desembolso = new Desembolso
                {
                    DtPrazo = AdicionarDiasUteis(DateTime.Today),
                    StatusDesembolso = TipoStatusDesembolso.Validar,
                    ResponsavelAnalise = null,
                    ResponsavelBaixa = null,
                    Gestor = null,
                };

                _context.FichaPedidoDesembolsos.Add(fpd);
                await _context.SaveChangesAsync(cancellationToken);

                var desembolsoEntrada = new DesembolsoEntrada
                {
                    CoGigov = request.CoGigov,
                    CoFpd = fpd.CoFpd,
                    DeTomador = request.MutuarioFinal,
                    CoTomador = request.CnpjMutuarioFinal,
                    DeAgentePromotor = request.AgentePromotor,
                    CoAgentePromotor = request.CnpjAgentePromotor,
                    ContratoAf = request.CoContratoAf,
                    DvAf = request.CoContratoAfDv,
                    DtEngenharia = request.DtEngenharia,
                    PercentualObra = (float)request.PercentualObra
                };
                _context.DesembolsoEntradas.Add(desembolsoEntrada);
                await _context.SaveChangesAsync(cancellationToken);

                coDesembolso = fpd.Desembolso.CoDesembolso;

                var validacoesModelo = await _context.CadastroValidacoes
                    .Where(x => !x.Desativado)
                    .OrderBy(x => x.CoValidacao)
                    .ToListAsync(cancellationToken);

                // Agrupa por validação SEM descartar — uma validação pode vir com mais
                // de um comentário (ex.: vários itens comentados na etapa Verificações).
                var registrosPorValidacao = request.ValidacoesDesembolsoRequest
                    .Where(x => x.ValidacaoRegistro is not null &&
                                !string.IsNullOrWhiteSpace(x.ValidacaoRegistro.DeRegistro))
                    .GroupBy(x => x.CoValidacao)
                    .ToDictionary(x => x.Key, x => x.ToList());

                foreach (var modelo in validacoesModelo)
                {
                    registrosPorValidacao.TryGetValue(modelo.CoValidacao, out var registrosDoItem);

                    // Mesma regra do AdicionarComentario avulso: se algum dos comentários
                    // já vier como Justificativa, o item nasce aprovado.
                    var temJustificativa = registrosDoItem?.Any(
                        r => r.ValidacaoRegistro.TipoRegistro == TipoRegistro.Justificativa) ?? false;

                    var validacaoDesembolso = new ValidacaoDesembolso
                    {
                        CoValidacao = modelo.CoValidacao,
                        CoDesembolso = coDesembolso,
                        DeValidacao = modelo.DeValidacao,
                        CampoVinculado = null, // analisar depois se precisa!
                        Situacao = temJustificativa ? TipoSituacaoValidacao.Aprovado : TipoSituacaoValidacao.Analisar,
                    };

                    _context.ValidacaoDesembolsos.Add(validacaoDesembolso);

                    if (registrosDoItem is null) continue;

                    foreach (var validacaoDesembolsoRequest in registrosDoItem)
                    {
                        var registro = new ValidacaoRegistro()
                        {
                            CoValidacao = validacaoDesembolsoRequest.CoValidacao,
                            CoDesembolso = coDesembolso,
                            CoUsuario = validacaoDesembolsoRequest.ValidacaoRegistro.MatriculaAutor,
                            DeUsuario = validacaoDesembolsoRequest.ValidacaoRegistro.NomeAutor,
                            UnidadeUsuario = validacaoDesembolsoRequest.ValidacaoRegistro.UnidadeAutor,
                            DtCriacao = DateTime.Now,
                            DeRegistro = validacaoDesembolsoRequest.ValidacaoRegistro.DeRegistro,
                            TipoRegistro = validacaoDesembolsoRequest.ValidacaoRegistro.TipoRegistro,
                        };

                        _context.RegistroValidacoes.Add(registro);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                var erroCompleto = ex.InnerException?.InnerException?.Message
                    ?? ex.InnerException?.Message
                    ?? ex.Message;
                throw new Exception($"Erro: {erroCompleto}");
            }

            // Fora do try/catch de propósito: a essa altura a FPD já foi
            // commitada — cair no catch acima tentaria dar rollback numa
            // transação já concluída. Notifica só quem pode agir (coordenação
            // 06/CEFGA) — GIGOV não precisa saber que uma FPD entrou na fila.
            await _notificacoes.EnviarCoordenacaoAsync(
                CodigoCoordenacaoCefga,
                "Novo desembolso inserido",
                $"Contrato {request.CoContratoAf}/{request.CoContratoAfDv} entrou na fila de análise.",
                link: LinkDesembolso(coDesembolso),
                cancellationToken: cancellationToken);
        }

        public async Task ReenviarFichaPedidoDesembolso(int coFpd, PedidoDesembolsoRequest request, CancellationToken cancellationToken = default)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);

            FichaPedidoDesembolso fpd;

            try
            {
                fpd = await _context.FichaPedidoDesembolsos
                    .Include(x => x.Desembolso)
                        .ThenInclude(d => d.ValidacoesDesembolso)
                    .FirstOrDefaultAsync(x => x.CoFpd == coFpd, cancellationToken);

                if (fpd is null)
                    throw new Exception($"FPD {coFpd} não encontrada.");

                // ── Atualiza os dados da ficha (CoFpd e DtSolicitado não mudam) ──
                AtualizarDadosFicha(fpd, request);

                // ── Volta o status pro início — precisa validar tudo de novo ──
                fpd.Desembolso.StatusDesembolso = TipoStatusDesembolso.Validar;
                fpd.Desembolso.DtConclusao = null;

                // ── Renova o prazo (D+2 dias úteis) — só acontece aqui, porque
                // só quem preenche/edita a FPD (empregado do setor que solicita)
                // dispara isso. Ações do analista (comentar, aprovar, rejeitar)
                // não passam por este método e não mexem no prazo.
                fpd.Desembolso.DtPrazo = AdicionarDiasUteis(DateTime.Today);

                // ── Reseta o checklist (TB005). O histórico de comentários em
                //    TB006 não é apagado nem alterado aqui. ──
                foreach (var validacao in fpd.Desembolso.ValidacoesDesembolso)
                {
                    validacao.Situacao = TipoSituacaoValidacao.Analisar;
                }

                // ── Sincroniza com o catálogo atual: itens de validação ativos
                // criados DEPOIS da FPD original (ex.: novas regras) não existem
                // ainda na TB005 dela — sem isso, o checklist do reenvio ficava
                // congelado no conjunto de itens de quando a FPD foi criada. ──
                var coValidacaoJaExistentes = fpd.Desembolso.ValidacoesDesembolso
                    .Select(v => v.CoValidacao)
                    .ToHashSet();

                var novosModelos = await _context.CadastroValidacoes
                    .Where(x => !x.Desativado && !coValidacaoJaExistentes.Contains(x.CoValidacao))
                    .ToListAsync(cancellationToken);

                foreach (var modelo in novosModelos)
                {
                    _context.ValidacaoDesembolsos.Add(new ValidacaoDesembolso
                    {
                        CoValidacao = modelo.CoValidacao,
                        CoDesembolso = fpd.Desembolso.CoDesembolso,
                        DeValidacao = modelo.DeValidacao,
                        Situacao = TipoSituacaoValidacao.Analisar,
                    });
                }

                // ── Comentário(s) novo(s) enviados junto da correção (opcional) ──
                // Agrupa sem descartar — pode vir mais de um comentário por validação.
                // Situação continua sempre resetada pra Analisar (bloco acima): reenvio
                // de FPD precisa ser revalidado do zero, não aprova nada automaticamente.
                var registrosPorValidacao = request.ValidacoesDesembolsoRequest
                    .Where(x => x.ValidacaoRegistro is not null &&
                                !string.IsNullOrWhiteSpace(x.ValidacaoRegistro.DeRegistro))
                    .GroupBy(x => x.CoValidacao)
                    .ToDictionary(x => x.Key, x => x.ToList());

                foreach (var validacao in fpd.Desembolso.ValidacoesDesembolso)
                {
                    if (!registrosPorValidacao.TryGetValue(validacao.CoValidacao, out var registrosDoItem))
                    {
                        continue;
                    }

                    foreach (var validacaoRequest in registrosDoItem)
                    {
                        var registro = new ValidacaoRegistro
                        {
                            CoValidacao = validacao.CoValidacao,
                            CoDesembolso = validacao.CoDesembolso,
                            DeRegistro = validacaoRequest.ValidacaoRegistro.DeRegistro,
                            TipoRegistro = validacaoRequest.ValidacaoRegistro.TipoRegistro,
                            CoUsuario = validacaoRequest.ValidacaoRegistro.MatriculaAutor,
                            DeUsuario = validacaoRequest.ValidacaoRegistro.NomeAutor,
                            UnidadeUsuario = validacaoRequest.ValidacaoRegistro.UnidadeAutor,
                            DtCriacao = DateTime.Now,
                        };
                        _context.RegistroValidacoes.Add(registro);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Erro ao reenviar FPD {coFpd}: {ex.Message}");
            }

            // Fora do try/catch pelo mesmo motivo do CriarFichaPedidoDesembolso.
            // GIGOV corrigiu e reenviou — volta pro início do ciclo, CEFGA
            // precisa saber que tem análise nova esperando de novo.
            await _notificacoes.EnviarCoordenacaoAsync(
                CodigoCoordenacaoCefga,
                "Ficha reenviada para nova análise",
                $"Contrato {fpd.CoContratoAf}/{fpd.CoContratoAfDv} foi corrigido e voltou para análise.",
                link: LinkDesembolso(fpd.Desembolso.CoDesembolso),
                cancellationToken: cancellationToken);
        }

        // ── Copia os dados editáveis do request pra ficha já existente.
        // CoFpd e DtSolicitado ficam de fora de propósito (não mudam no reenvio). ──
        private static void AtualizarDadosFicha(FichaPedidoDesembolso fpd, PedidoDesembolsoRequest request)
        {
            fpd.MatriculaSolicitante = request.MatriculaSolicitante;
            fpd.CoGigov = request.CoGigov;
            fpd.MatriculaGestor = request.MatriculaGestor;
            fpd.CoContratoAf = request.CoContratoAf;
            fpd.CoContratoAfDv = request.CoContratoAfDv;
            fpd.PrimeiroDesembolso = request.PrimeiroDesembolso;
            fpd.AgenteFinanceiro = request.AgenteFinanceiro;
            fpd.CnpjAf = request.CnpjAf;
            fpd.MutuarioFinal = request.MutuarioFinal;
            fpd.CnpjMutuarioFinal = request.CnpjMutuarioFinal;
            fpd.AgenteTecnicoOperador = request.AgenteTecnicoOperador;
            fpd.CnpjAgenteTecnicoOperador = request.CnpjAgenteTecnicoOperador;
            fpd.AgentePromotor = request.AgentePromotor;
            fpd.CnpjAgentePromotor = request.CnpjAgentePromotor;
            fpd.Programa = (Programa)request.Programa;
            fpd.UltimoDesembolso = request.UltimoDesembolso;
            fpd.Funcionalidade = request.Funcionalidade;
            fpd.Concluido = request.Concluido;
            fpd.DtEngenharia = request.DtEngenharia;
            fpd.SituacaoObra = (TipoSituacaoObra)request.SituacaoObra;
            fpd.DtSocioAmbiental = request.DtSocioAmbiental;
            fpd.PercentualObra = request.PercentualObra;
            fpd.TipoDesembolso = (TipoDesembolso)request.TipoDesembolso;
            fpd.RetornoParcial = request.RetornoParcial;
            fpd.PlacaLocal = request.PlacaLocal;
            fpd.LicensaInstalacao = request.LicensaInstalacao;
            fpd.LicensaOperacao = request.LicensaOperacao;
            fpd.SolicitadoVi = request.SolicitadoVi;
            fpd.GlossadoVi = request.GlossadoVi;
            fpd.AceitoVi = request.AceitoVi;
            fpd.ParticipacaoFgts = request.ParticipacaoFgts;
            fpd.Contrapartida = request.Contrapartida;
            fpd.ValorEmprestimo = request.ValorEmprestimo;
            fpd.Desembolsado = request.Desembolsado;
            fpd.SaldoADesembolsar = request.SaldoADesembolsar;
            fpd.Excepcionalizado = request.Excepcionalizado;
            fpd.ContrapartidaAtual = request.ContrapartidaAtual;
            fpd.Integralizado = request.Integralizado;
            fpd.SaldoAIntegralizar = request.SaldoAIntegralizar;
            fpd.ContrapartidaAlterada = request.ContrapartidaAlterada;
            fpd.Amortizacao = request.Amortizacao;
            fpd.Sanepar = request.Sanepar;
        }

        public async Task AdicionarComentario(ValidacaoDesembolsoRequest request, CancellationToken cancellationToken = default)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (request.ValidacaoRegistro is null ||
                string.IsNullOrWhiteSpace(request.ValidacaoRegistro.DeRegistro))
            {
                throw new Exception("O comentário não pode ser vazio.");
            }

            var validacao = await _context.ValidacaoDesembolsos
                .FirstOrDefaultAsync(x => x.CoValidacao == request.CoValidacao
                                        && x.CoDesembolso == request.CoDesembolso, cancellationToken);

            if (validacao is null)
                throw new Exception($"Validação {request.CoValidacao} do desembolso {request.CoDesembolso} não encontrada.");


            if (request.ValidacaoRegistro.TipoRegistro == TipoRegistro.Justificativa)
            {
                validacao.Situacao = TipoSituacaoValidacao.Aprovado;
            }

            var registro = new ValidacaoRegistro
            {
                CoValidacao = request.CoValidacao,
                CoDesembolso = request.CoDesembolso,

                DeRegistro = request.ValidacaoRegistro.DeRegistro.Trim(),
                TipoRegistro = request.ValidacaoRegistro.TipoRegistro,
                CoUsuario = request.ValidacaoRegistro.MatriculaAutor,
                DeUsuario = request.ValidacaoRegistro.NomeAutor,
                UnidadeUsuario = request.ValidacaoRegistro.UnidadeAutor,
                DtCriacao = DateTime.Now,
            };

            await _context.RegistroValidacoes.AddAsync(registro, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await NotificarComentarioInserido(request.CoDesembolso, request.ValidacaoRegistro.UnidadeAutor, cancellationToken);
        }

        // Notifica sempre o "outro lado" de quem comentou: CEFGA comentou →
        // avisa o solicitante (GIGOV) da FPD, individualmente; GIGOV comentou →
        // avisa a coordenação CEFGA inteira (não tem um analista único fixo até
        // alguém se vincular como ResponsavelAnalise).
        private async Task NotificarComentarioInserido(int coDesembolso, int unidadeAutor, CancellationToken cancellationToken = default)
        {
            if (unidadeAutor == 7175)
            {
                var matriculaSolicitante = await _context.Desembolsos
                    .Where(x => x.CoDesembolso == coDesembolso)
                    .Select(x => x.FichaPedidoDesembolso.MatriculaSolicitante)
                    .FirstOrDefaultAsync(cancellationToken);

                if (string.IsNullOrWhiteSpace(matriculaSolicitante))
                    return;

                await _notificacoes.EnviarIndividualAsync(
                    "Novo comentário no seu desembolso",
                    "A CEFGA registrou um comentário que precisa da sua atenção.",
                    [matriculaSolicitante],
                    CodigoAplicativo.Cad,
                    link: LinkDesembolso(coDesembolso),
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _notificacoes.EnviarCoordenacaoAsync(
                    CodigoCoordenacaoCefga,
                    "Novo comentário para análise",
                    "O solicitante (GIGOV) registrou um comentário no desembolso.",
                    link: LinkDesembolso(coDesembolso),
                    cancellationToken: cancellationToken);
            }
        }

        public async Task ADicionarCOmentario2(ValidacaoDesembolsoRequest request)
        {
           if(request == null) throw new ArgumentNullException(nameof(request));

            var validacao = await _context.ValidacaoDesembolsos
                .Where(v => v.CoValidacao  == request.CoValidacao
                 && v.CoDesembolso == request.CoDesembolso)
                .FirstOrDefaultAsync();

            if (validacao == null) throw new Exception($"Desembolso não encontrado!");

            if(request.ValidacaoRegistro.TipoRegistro == TipoRegistro.Justificativa)
            {
                validacao.Situacao = TipoSituacaoValidacao.Aprovado;
            }
        }

        public async Task ValidarDesembolso(int coDesembolso, CancellationToken cancellationToken = default)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var desembolso = await _context.Desembolsos
                    .Include(x => x.FichaPedidoDesembolso)
                    .Include(x => x.ValidacoesDesembolso)
                    .FirstOrDefaultAsync(x => x.CoDesembolso == coDesembolso, cancellationToken);

                if (desembolso is null)
                    throw new Exception($"Desembolso {coDesembolso} não encontrado.");

                await ExecutarValidacaoDesembolso(desembolso);

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Erro ao validar automaticamente o desembolso {coDesembolso}: {ex.Message}");
            }
        }

        public async Task ValidarTodosPendentes(CancellationToken cancellationToken = default)
        {
            var desembolsos = await _context.Desembolsos
                .Include(x => x.FichaPedidoDesembolso)
                .Include(x => x.ValidacoesDesembolso)
                .Where(x => x.StatusDesembolso == TipoStatusDesembolso.Validar)
                .ToListAsync(cancellationToken);

            if (desembolsos.Count == 0)
                return;

            await using var transaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                foreach (var desembolso in desembolsos)
                {
                    await ExecutarValidacaoDesembolso(desembolso);
                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Erro ao validar desembolsos pendentes: {ex.Message}");
            }
        }

        // Versão simplificada, a pedido do usuário: só empurra o status adiante
        // (Validar/pendência → Analisar), sem rodar a avaliação real por regra.
        // A chamada de verdade fica comentada abaixo — é só descomentar pra
        // voltar ao comportamento original (cada item do checklist
        // Aprovado/Analisar conforme o resultado da regra).
        private async Task ExecutarValidacaoDesembolso(Desembolso desembolso)
        {
            // var resultados = await _validador.Validar(desembolso.FichaPedidoDesembolso);
            //
            // foreach (var resultado in resultados)
            // {
            //     var validacao = desembolso.ValidacoesDesembolso
            //         .FirstOrDefault(x => x.CoValidacao == resultado.CoValidacao);
            //
            //     if (validacao is null)
            //         continue; // código que o validador não reconhece nesse desembolso
            //
            //     validacao.Situacao = resultado.Aprovado
            //         ? TipoSituacaoValidacao.Aprovado
            //         : TipoSituacaoValidacao.Analisar;
            // }

            desembolso.StatusDesembolso = TipoStatusDesembolso.Analisar;
        }

        public async Task<List<DesembolsoResponse>> ObterTodosDesembolsos(CancellationToken cancellationToken = default)
        {
            var desembolsos = await _context.Desembolsos
                .Include(x => x.FichaPedidoDesembolso)
                .Include(x => x.ValidacoesDesembolso)
                .ToListAsync(cancellationToken);

            return desembolsos.Select(d =>
            {
                var totalValidacoes = d.ValidacoesDesembolso.Count;
                var totalAprovadas = d.ValidacoesDesembolso.Count(v => v.Situacao == TipoSituacaoValidacao.Aprovado);

                return new DesembolsoResponse
                {
                    CoDesembolso = d.CoDesembolso,
                    Id = $"Desembolso-{d.CoDesembolso}",
                    NumId = d.CoFpd.ToString(),
                    DtSolicitado = d.FichaPedidoDesembolso.DtSolicitado,
                    Contrato = $"{d.FichaPedidoDesembolso.CoContratoAf}/{d.FichaPedidoDesembolso.CoContratoAfDv}",
                    Mutuario = d.FichaPedidoDesembolso.MutuarioFinal,
                    Gigov = d.FichaPedidoDesembolso.CoGigov,
                    PrimeiroDesembolso = d.FichaPedidoDesembolso.PrimeiroDesembolso,
                    Adiantamento = d.FichaPedidoDesembolso.TipoDesembolso == TipoDesembolso.Adiantamento,
                    UltimoDesembolso = d.FichaPedidoDesembolso.UltimoDesembolso,
                    Valor = d.FichaPedidoDesembolso.ParticipacaoFgts,
                    ValidacoesOk = totalAprovadas,
                    ValidacoesTotal = totalValidacoes,
                    Status = d.StatusDesembolso,
                    PrazoFinal = d.DtPrazo,
                    ResponsavelAnalise = d.ResponsavelAnalise,
                    DtConclusao = d.DtConclusao,
                    // Sanepar entra direto no filtro "Agendado" da Home — não
                    // muda o StatusDesembolso (segue Validar/Analisar/BaixarDRP
                    // normal por trás).
                    //
                    // REGRA AINDA NÃO IMPLEMENTADA (pedido do usuário: deixar
                    // comentada por enquanto): desembolsos Sanepar são
                    // adicionados em qualquer dia, mas só devem sair da lista
                    // "Agendado" e entrar na lista normal de análise no dia 20
                    // de cada mês. Quando for implementar, algo como:
                    //
                    //   if (d.FichaPedidoDesembolso.Sanepar == true
                    //       && DateTime.Today.Day >= 20
                    //       && d.StatusDesembolso == TipoStatusDesembolso.Validar)
                    //   {
                    //       // mover pra fila normal — mudar o status (ou um
                    //       // campo à parte, tipo LiberadoParaAnalise) pra
                    //       // ObterTodosDesembolsos parar de tratar como
                    //       // agendado a partir daqui.
                    //   }
                    DataAgendamento = d.FichaPedidoDesembolso.Sanepar == true,
                };
            }).ToList();
        }

        #region TESTADO E FUNCIONANDO

        public async Task EditarComentario(EditarComentarioRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Texto))
                throw new Exception("O comentário não pode ser vazio.");

            var registro = await _context.RegistroValidacoes
                .FirstOrDefaultAsync(x => x.CoRegistroValidacao == request.CoRegistroValidacao, cancellationToken);

            if (registro is null)
                throw new Exception($"Comentário {request.CoRegistroValidacao} não encontrado.");

            if (string.IsNullOrEmpty(registro.CoUsuario) || registro.CoUsuario != request.MatriculaSolicitante)
                throw new UnauthorizedAccessException("Apenas o autor do comentário pode editá-lo.");

            registro.DeRegistro = request.Texto.Trim();
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Soft-delete: nunca apaga a linha (histórico fica intacto pra
        // auditoria), só marca Ativo=false — ObterComentarios/ObterDetalheDesembolso
        // já filtram por isso, então some do front imediatamente.
        public async Task RemoverComentario(int coRegistroValidacao, string matriculaSolicitante, CancellationToken cancellationToken = default)
        {
            var registro = await _context.RegistroValidacoes
                .FirstOrDefaultAsync(x => x.CoRegistroValidacao == coRegistroValidacao, cancellationToken);

            if (registro is null)
                throw new Exception($"Comentário {coRegistroValidacao} não encontrado.");

            if (string.IsNullOrEmpty(registro.CoUsuario) || registro.CoUsuario != matriculaSolicitante)
                throw new UnauthorizedAccessException("Apenas o autor do comentário pode removê-lo.");

            registro.Ativo = false;

            // Se esse comentário era uma Justificativa (o tipo que aprova o item
            // sozinho — ver AdicionarComentario) e não sobrou nenhuma outra
            // Justificativa ativa pra essa validação, desfaz a aprovação
            // automática: volta pra Analisar. Conservador de propósito — nunca
            // reprova (Negado) sozinho, só desfaz o que tinha sido aprovado só
            // por causa desse comentário que acabou de sumir.
            if (registro.TipoRegistro == TipoRegistro.Justificativa)
            {
                var aindaTemJustificativaAtiva = await _context.RegistroValidacoes
                    .AnyAsync(x => x.CoValidacao == registro.CoValidacao
                                && x.CoDesembolso == registro.CoDesembolso
                                && x.Ativo
                                && x.TipoRegistro == TipoRegistro.Justificativa
                                && x.CoRegistroValidacao != registro.CoRegistroValidacao, cancellationToken);

                if (!aindaTemJustificativaAtiva)
                {
                    var validacao = await _context.ValidacaoDesembolsos
                        .FirstOrDefaultAsync(x => x.CoValidacao == registro.CoValidacao
                                                && x.CoDesembolso == registro.CoDesembolso, cancellationToken);

                    if (validacao is not null && validacao.Situacao == TipoSituacaoValidacao.Aprovado)
                    {
                        validacao.Situacao = TipoSituacaoValidacao.Analisar;
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task VincularResponsavel(int coDesembolso, string? matriculaResponsavel, CancellationToken cancellationToken = default)
        {
            var desembolso = await _context.Desembolsos
                .FirstOrDefaultAsync(x => x.CoDesembolso == coDesembolso, cancellationToken);

            if (desembolso is null)
                throw new Exception($"Desembolso {coDesembolso} não encontrado.");

            if (!string.IsNullOrWhiteSpace(matriculaResponsavel))
            {
                var empregado = await _empregados.ObterPorMatricula(matriculaResponsavel);
                if (empregado is null)
                    throw new Exception($"Empregado {matriculaResponsavel} não encontrado.");
            }

            desembolso.ResponsavelAnalise = matriculaResponsavel;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ValidacaoTemplateResponse>> ObterValidacoesTemplate(CancellationToken cancellationToken = default)
        {
            return await _context.CadastroValidacoes
                .Where(x => !x.Desativado)
                .OrderBy(x => x.CoValidacao)
                .Select(x => new ValidacaoTemplateResponse
                {
                    CoValidacao = x.CoValidacao,
                    DeValidacao = x.DeValidacao,
                })
                .ToListAsync(cancellationToken);
        }
        #endregion

        public async Task AprovarDesembolso(int coDesembolso, AprovarDesembolsoRequest request, CancellationToken cancellationToken = default)
        {
            var desembolso = await _context.Desembolsos
                .Include(x => x.ValidacoesDesembolso)
                .Include(x => x.FichaPedidoDesembolso)
                .FirstOrDefaultAsync(x => x.CoDesembolso == coDesembolso, cancellationToken);

            if (desembolso is null)
                throw new Exception($"Desembolso {coDesembolso} não encontrado.");

            var pendencias = desembolso.ValidacoesDesembolso
                .Where(x => x.Situacao != TipoSituacaoValidacao.Aprovado)
                .ToList();

            if (pendencias.Count > 0)
                throw new Exception(
                    $"Não é possível aprovar: {pendencias.Count} validação(ões) ainda não aprovada(s).");

            desembolso.StatusDesembolso = TipoStatusDesembolso.BaixarDRP;
            desembolso.DtConclusao = DateTime.Now;
            desembolso.ResponsavelBaixa = request.MatriculaUsuario;

            await _context.SaveChangesAsync(cancellationToken);

            await _notificacoes.EnviarIndividualAsync(
                "Desembolso aprovado",
                $"Contrato {desembolso.FichaPedidoDesembolso.CoContratoAf}/{desembolso.FichaPedidoDesembolso.CoContratoAfDv} foi aprovado e aguarda baixa da DRP.",
                [desembolso.FichaPedidoDesembolso.MatriculaSolicitante],
                CodigoAplicativo.Cad,
                link: LinkDesembolso(coDesembolso),
                cancellationToken: cancellationToken);
        }

        public async Task BaixarDRP(int coDesembolso, CancellationToken cancellationToken = default)
        {
            var desembolso = await _context.Desembolsos
                .Include(x => x.FichaPedidoDesembolso)
                .FirstOrDefaultAsync(x => x.CoDesembolso == coDesembolso, cancellationToken);

            if (desembolso is null)
                throw new Exception($"Desembolso {coDesembolso} não encontrado.");

            desembolso.StatusDesembolso = TipoStatusDesembolso.Finalizado;

            await _context.SaveChangesAsync(cancellationToken);

            await _notificacoes.EnviarIndividualAsync(
                "DRP baixada",
                $"A DRP do contrato {desembolso.FichaPedidoDesembolso.CoContratoAf}/{desembolso.FichaPedidoDesembolso.CoContratoAfDv} foi baixada.",
                [desembolso.FichaPedidoDesembolso.MatriculaSolicitante],
                CodigoAplicativo.Cad,
                link: LinkDesembolso(coDesembolso),
                cancellationToken: cancellationToken);
        }

        // Aba DRP (controle de baixa): todo desembolso que já foi aprovado —
        // aguardando baixa (BaixarDRP) ou já baixado (Finalizado). Antes disso
        // (Validar/Analisar) não entra aqui; é a lista de análise normal.
        public async Task<List<RegistroDrpResponse>> ObterRegistrosDrp(CancellationToken cancellationToken = default)
        {
            var desembolsos = await _context.Desembolsos
                .Include(x => x.FichaPedidoDesembolso)
                .Where(x => x.StatusDesembolso == TipoStatusDesembolso.BaixarDRP
                         || x.StatusDesembolso == TipoStatusDesembolso.Finalizado)
                .ToListAsync(cancellationToken);

            return desembolsos.Select(d => new RegistroDrpResponse
            {
                Id = d.CoDesembolso,
                Gigov = d.FichaPedidoDesembolso.CoGigov,
                ContratoDv = $"{d.FichaPedidoDesembolso.CoContratoAf}-{d.FichaPedidoDesembolso.CoContratoAfDv}",
                TipoDesembolso = d.FichaPedidoDesembolso.TipoDesembolso == TipoDesembolso.Adiantamento ? "adiantamento" : "normal",
                ValorFgts = d.FichaPedidoDesembolso.ParticipacaoFgts,
                DataSolicitacao = d.FichaPedidoDesembolso.DtSolicitado,
                Responsavel = d.ResponsavelBaixa ?? string.Empty,
                Gestor = d.FichaPedidoDesembolso.MatriculaGestor,
                Baixa = d.MatriculaBaixa,
            }).ToList();
        }

        // Confirma a baixa de um ou mais registros da aba DRP. Só desembolsos
        // ainda em BaixarDRP (aguardando) podem ser baixados — ids em qualquer
        // outro status são ignorados silenciosamente (ex.: duplo clique, ou
        // seleção que já incluía um item baixado por outra pessoa entretanto).
        public async Task BaixarDrpEmLote(BaixarDrpRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Ids.Count == 0)
                throw new Exception("Nenhum registro selecionado para baixa.");

            if (string.IsNullOrWhiteSpace(request.MatriculaUsuario))
                throw new Exception("Matrícula do usuário é obrigatória para confirmar a baixa.");

            var desembolsos = await _context.Desembolsos
                .Include(x => x.FichaPedidoDesembolso)
                .Where(x => request.Ids.Contains(x.CoDesembolso)
                         && x.StatusDesembolso == TipoStatusDesembolso.BaixarDRP)
                .ToListAsync(cancellationToken);

            foreach (var desembolso in desembolsos)
            {
                desembolso.StatusDesembolso = TipoStatusDesembolso.Finalizado;
                desembolso.MatriculaBaixa = request.MatriculaUsuario;
            }

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var desembolso in desembolsos)
            {
                await _notificacoes.EnviarIndividualAsync(
                    "DRP baixada",
                    $"A DRP do contrato {desembolso.FichaPedidoDesembolso.CoContratoAf}/{desembolso.FichaPedidoDesembolso.CoContratoAfDv} foi baixada.",
                    [desembolso.FichaPedidoDesembolso.MatriculaSolicitante],
                    CodigoAplicativo.Cad,
                    link: LinkDesembolso(desembolso.CoDesembolso),
                    cancellationToken: cancellationToken);
            }
        }

        public async Task RejeitarDesembolso(int coDesembolso, RejeitarDesembolsoRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.CodigoCoordenacao))
                throw new Exception("Código da coordenação é obrigatório para rejeitar.");

            if (string.IsNullOrWhiteSpace(request.Justificativa))
                throw new Exception("A justificativa da rejeição é obrigatória.");

            var desembolso = await _context.Desembolsos
                .Include(x => x.FichaPedidoDesembolso)
                .FirstOrDefaultAsync(x => x.CoDesembolso == coDesembolso, cancellationToken);

            if (desembolso is null)
                throw new Exception($"Desembolso {coDesembolso} não encontrado.");

            desembolso.StatusDesembolso = TipoStatusDesembolso.Negado;
            desembolso.DtConclusao = DateTime.Now;
            desembolso.ResponsavelBaixa = request.MatriculaUsuario;

            // Justificativa vira um ValidacaoRegistro normal (mesma tabela/rastro de
            // autoria dos comentários de checklist). CoValidacao aponta pro
            // cadastro real "Pedido Negado" (desativado de propósito — não é
            // item de checklist, só uma âncora no catálogo pra não usar um
            // CoValidacao = 0 inventado, que não correspondia a nada em
            // CadastroValidacao e não tinha FK nenhuma garantindo isso).
            var validacaoPedidoNegado = await _context.CadastroValidacoes
                .FirstOrDefaultAsync(x => x.DeValidacao == PedidoNegadoDeValidacao, cancellationToken);

            if (validacaoPedidoNegado is null)
                throw new Exception(
                    $"Cadastro de validação '{PedidoNegadoDeValidacao}' não encontrado — necessário para registrar a justificativa da rejeição.");

            _context.RegistroValidacoes.Add(new ValidacaoRegistro
            {
                CoValidacao = validacaoPedidoNegado.CoValidacao,
                CoDesembolso = coDesembolso,
                DeRegistro = request.Justificativa.Trim(),
                TipoRegistro = TipoRegistro.Rejeicao,
                CoUsuario = request.MatriculaUsuario,
                DeUsuario = request.UsuarioNome,
                // Rejeitar é ação exclusiva de CEFGA (regra de negócio — o botão nem
                // aparece pra GIGOV no front). Fixo em 7175 aqui: não depende do que
                // o cliente manda, e RejeitarDesembolsoRequest não precisa de mais um campo.
                UnidadeUsuario = 7175,
                DtCriacao = DateTime.Now,
            });

            await _context.SaveChangesAsync(cancellationToken);

            await _notificacoes.EnviarIndividualAsync(
                "Desembolso rejeitado",
                $"Contrato {desembolso.FichaPedidoDesembolso.CoContratoAf}/{desembolso.FichaPedidoDesembolso.CoContratoAfDv} foi rejeitado.",
                [desembolso.FichaPedidoDesembolso.MatriculaSolicitante],
                CodigoAplicativo.Cad,
                link: LinkDesembolso(coDesembolso),
                cancellationToken: cancellationToken);
        }

        // Nome fixo usado pra localizar o cadastro-âncora de "Pedido Negado"
        // em CadastroValidacoes — cadastrado como Desativado=true (não vira
        // item de checklist nem aparece em ObterValidacoesTemplate).
        private const string PedidoNegadoDeValidacao = "Pedido Negado";

        public async Task RejeitarDesembolsoTeste(int idDesembolso)
        {
            var desembolso = await _context.Desembolsos
            .FirstOrDefaultAsync(d => d.CoDesembolso == idDesembolso);
            if (desembolso is null)
                throw new Exception($"Desembolso {idDesembolso} não encontrado.");
        }

        // Prazo do desembolso é sempre D+2 dias úteis — regra fixa do sistema,
        // não varia por tipo de fluxo nem por configuração.
        private const int PrazoDesembolsoDiasUteis = 2;

        private static DateTime AdicionarDiasUteis(DateTime dataBase)
        {
            var data = dataBase;
            var adicionados = 0;
            while (adicionados < PrazoDesembolsoDiasUteis)
            {
                data = data.AddDays(1);
                if (data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday)
                    adicionados++;
            }
            return data;
        }
    }
}
