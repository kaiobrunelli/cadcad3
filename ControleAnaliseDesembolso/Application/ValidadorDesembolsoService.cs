using ControleAnaliseDesembolso.Application.Dtos.Response;
using ControleAnaliseDesembolso.Application.Interface;
using ControleAnaliseDesembolso.Domain.Entitys;
using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Application
{
    public class ValidadorDesembolsoService : IValidadorDesembolsoService
    {
        private readonly IValoresReferenciaContratoService _valoresReferencia;

        public ValidadorDesembolsoService(IValoresReferenciaContratoService valoresReferencia)
        {
            _valoresReferencia = valoresReferencia;
        }

        // Catálogo (CadastroValidacao) foi alinhado pra espelhar 1:1 os itens
        // Sim/Não/NSA da ficha de FPD (etapa "Verificações" do Preencher/Editar
        // FPD) — são a mesma coisa, só vista em dois lugares. "Validar" aqui
        // só confere se a GIGOV já marcou "Sim" pra cada campo e, se sim,
        // aprova automaticamente o item correspondente no checklist de
        // análise; se "Não" ou não respondido, o item fica pendente
        // ("Analisar") pra intervenção manual do analista.
        //
        // "Tomador adimplente" (3), "Agente Promotor adimplente" (novo) e
        // "Valores da FPD" (4) ficam de fora — nunca tiveram uma coluna
        // própria na ficha (TomadorAdimplente/PromotorAdimplente eram só
        // client-side, "Valores da FPD" é âncora de comentário/OBS AF) —,
        // então continuam manuais, via AdicionarComentario.
        private static readonly List<Func<FichaPedidoDesembolso, ResultadoValidacaoAutomatica>> _regras =
        [
            fpd => RegraBooleana(coValidacao: 1, valor: fpd.LicensaOperacao, nomeCampo: "Licença de operação"),
            fpd => RegraBooleana(coValidacao: 2, valor: fpd.LicensaInstalacao, nomeCampo: "Licença de instalação"),
            fpd => RegraBooleana(coValidacao: 6, valor: fpd.Amortizacao, nomeCampo: "Amortização"),
            fpd => RegraBooleana(coValidacao: 7, valor: fpd.RetornoParcial, nomeCampo: "Retorno parcial"),
            fpd => RegraBooleana(coValidacao: 8, valor: fpd.PlacaLocal, nomeCampo: "Placa local"),
            fpd => RegraBooleana(coValidacao: 9, valor: fpd.Excepcionalizado, nomeCampo: "Excepcionalização"),
            fpd => RegraBooleana(coValidacao: 1005, valor: fpd.ContrapartidaAlterada, nomeCampo: "CP alterada"),
        ];

        public Task<List<ResultadoValidacaoAutomatica>> Validar(FichaPedidoDesembolso fpd) =>
            Task.FromResult(_regras.Select(regra => regra(fpd)).ToList());

        // Auxiliar pra qualquer regra true/false — só diz qual campo checar.
        // Conservador de propósito: só aprova automaticamente quando "Sim"
        // (true); "Não" ou não respondido (null) deixa o item pendente
        // ("Analisar") pra decisão manual do analista — nunca reprova sozinho.
        private static ResultadoValidacaoAutomatica RegraBooleana(int coValidacao, bool? valor, string nomeCampo)
        {
            var aprovado = valor == true;
            return new ResultadoValidacaoAutomatica
            {
                CoValidacao = coValidacao,
                Aprovado = aprovado,
                Mensagem = aprovado ? $"{nomeCampo}: OK." : $"{nomeCampo}: pendente ou não informado.",
            };
        }
    }
}
