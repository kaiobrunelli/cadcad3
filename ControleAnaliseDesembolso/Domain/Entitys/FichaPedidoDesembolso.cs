using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Domain.Entitys
{
    public class FichaPedidoDesembolso
    {
        public int CoFpd { get; set; }
        public string MatriculaSolicitante { get; set; } = string.Empty;

        public string CoGigov { get; set; } = string.Empty; // string porque o código pode começar com zero
        public string MatriculaGestor { get; set; } = string.Empty;
        public DateTime DtSolicitado { get; set; }

        public string CoContratoAf { get; set; } = string.Empty;
        public string CoContratoAfDv { get; set; } = string.Empty;
        public bool PrimeiroDesembolso { get; set; }
        public string AgenteFinanceiro { get; set; } = string.Empty;
        public string CnpjAf { get; set; } = string.Empty;
        public string MutuarioFinal { get; set; } = string.Empty;
        public string CnpjMutuarioFinal { get; set; } = string.Empty;

        public string? AgenteTecnicoOperador { get; set; }
        public string? CnpjAgenteTecnicoOperador { get; set; }
        public string AgentePromotor { get; set; } = string.Empty;
        public string CnpjAgentePromotor { get; set; } = string.Empty;
        public Programa Programa { get; set; }
        public bool UltimoDesembolso { get; set; }
        public bool? Funcionalidade { get; set; }
        public DateTime? Concluido { get; set; } // TODO: decidir com o time — client manda "sim/nao/nsa" (Conclusao), aqui é data.
        public DateTime DtEngenharia { get; set; }
        public TipoSituacaoObra? SituacaoObra { get; set; }

        public DateTime? DtSocioAmbiental { get; set; }
        public decimal PercentualObra { get; set; }
        public TipoDesembolso TipoDesembolso { get; set; }
        public bool? RetornoParcial { get; set; }
        public bool? PlacaLocal { get; set; }
        public bool? LicensaInstalacao { get; set; }
        public bool? LicensaOperacao { get; set; }
        public decimal SolicitadoVi { get; set; }
        public decimal GlossadoVi { get; set; }
        public decimal AceitoVi { get; set; }

        public decimal ParticipacaoFgts { get; set; }
        public decimal Contrapartida { get; set; }
        public decimal ValorEmprestimo { get; set; }
        public decimal Desembolsado { get; set; }
        public decimal SaldoADesembolsar { get; set; }
        public bool? Excepcionalizado { get; set; }
        public decimal ContrapartidaAtual { get; set; }
        public decimal Integralizado { get; set; }
        public decimal SaldoAIntegralizar { get; set; }
        public bool? ContrapartidaAlterada { get; set; }

        // Só relevante quando UltimoDesembolso = true — usado pela validação
        // automática "Amortização confirmada no último desembolso".
        public bool? Amortizacao { get; set; }

        // Sanepar = true não muda o StatusDesembolso — o desembolso segue o
        // fluxo normal de análise (Validar/Analisar/BaixarDRP). Só marca a
        // origem pra entrar no filtro "Agendado" da Home (ver
        // DesembolsoResponse.DataAgendamento). A regra real do negócio (que
        // move Sanepar pra lista de análise só no dia 20 de cada mês) NÃO
        // está implementada — ver comentário em
        // ControleAnaliseDesembolsoService.ObterTodosDesembolsos.
        public bool? Sanepar { get; set; }

        public Desembolso Desembolso { get; set; } = new();
    }
}
