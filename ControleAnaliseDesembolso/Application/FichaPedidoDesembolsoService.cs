using ControleAnaliseDesembolso.Application.Dtos.Request;
using ControleAnaliseDesembolso.Application.Dtos.Response;
using ControleAnaliseDesembolso.Application.Interface;
using ControleAnaliseDesembolso.Domain.Entitys;
using ControleAnaliseDesembolso.Domain.Enums;
using ControleAnaliseDesembolso.Domain.Repositorys;
using ControleAnaliseDesembolso.Infra.Data.Context;
using ControleAnaliseDesembolso.Infra.Data.Repositorys;

namespace ControleAnaliseDesembolso.Application
{
    public class FichaPedidoDesembolsoService : IFichaPedidoDesembolsoService
    {
        private readonly IRepositorioFichaPedidoDesembolso _repositorioFpd;

        public FichaPedidoDesembolsoService(ControleAnaliseDesembolsoContext context)
        {
            _repositorioFpd = new RepositorioFichaPedidoDesembolso(context);
        }

        // Mesmo conjunto de campos de ControleAnaliseDesembolsoService.MapearParaFicha —
        // os dois serviços não compartilham esse helper hoje (não introduzi
        // esse acoplamento novo), só a lista de campos é igual porque o DTO
        // de origem é o mesmo.
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
            };
        }

        public async Task SalvarFpd(PedidoDesembolsoRequest Fpd, CancellationToken cancellationToken = default)
        {
            try
            {
                var fpd = MapearParaFicha(Fpd);
                fpd.DtSolicitado = DateTime.Now;

                await _repositorioFpd.Adicionar(fpd, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar salvar a Ficha de Pedido De Desembolso: {Fpd}. {ex.Message}");
            }
        }

        public async Task<PedidoConsultaContratoAfResponse> SolicitarDadosFPD(PedidoConsultaContratoAfRequest Pedido, CancellationToken cancellationToken = default)
        {
            var FpdAnterior = await _repositorioFpd.ObterContrato(x =>
                                        x.CoContratoAf == Pedido.CoContratoAf &&
                                        x.CoContratoAfDv == Pedido.CoContratoAfDv,
                                        x => x.CoFpd,
                                        cancellationToken);

            if (FpdAnterior is null)
            {
                throw new Exception($"Contrato: {Pedido.CoContratoAf}-{Pedido.CoContratoAfDv} não encontrado.");
            }

            return new PedidoConsultaContratoAfResponse
            {
                CoContratoAf = FpdAnterior.CoContratoAf,
                CoContratoAfDv = FpdAnterior.CoContratoAfDv,
                // NuFpd: entidade não tem mais esse campo (era incrementado aqui
                // antes, `FpdAnterior.NuFpd++`, já vinha comentado na origem) —
                // fica em 0 até essa regra ser retomada.
                AgenteFinanceiro = FpdAnterior.AgenteFinanceiro,
                MutuarioFinal = FpdAnterior.MutuarioFinal,
                AgenteTecnicoOperador = FpdAnterior.AgenteTecnicoOperador,
                AgentePromotor = FpdAnterior.AgentePromotor,
                Programa = FpdAnterior.Programa.ToString(),
                RetornoParcial = FpdAnterior.RetornoParcial ?? false,
                ValorEmprestimo = FpdAnterior.ValorEmprestimo,
                Desembolsado = FpdAnterior.Desembolsado,
                ContrapartidaAtual = FpdAnterior.ContrapartidaAtual,
                Integralizado = FpdAnterior.Integralizado,
            };
        }
    }
}
