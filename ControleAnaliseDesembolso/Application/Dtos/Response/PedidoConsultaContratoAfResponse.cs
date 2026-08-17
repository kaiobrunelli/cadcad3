namespace ControleAnaliseDesembolso.Application.Dtos.Response
{
    public class PedidoConsultaContratoAfResponse
    {
        public string CoContratoAf { get; set; } = string.Empty;
        public string CoContratoAfDv { get; set; } = string.Empty;
        public int NuFpd { get; set; }
        public string AgenteFinanceiro { get; set; } = string.Empty;
        public string MutuarioFinal { get; set; } = string.Empty;
        public string? AgenteTecnicoOperador { get; set; }
        public string AgentePromotor { get; set; } = string.Empty;
        public string Programa { get; set; } = string.Empty;
        public bool RetornoParcial { get; set; }
        public decimal ValorEmprestimo { get; set; }
        public decimal Desembolsado { get; set; }
        public decimal ContrapartidaAtual { get; set; }
        public decimal Integralizado { get; set; }
    }
}
