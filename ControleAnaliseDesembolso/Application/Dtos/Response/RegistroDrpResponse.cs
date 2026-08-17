namespace ControleAnaliseDesembolso.Application.Dtos.Response
{
    public class RegistroDrpResponse
    {
        public int Id { get; set; }
        public string Gigov { get; set; } = string.Empty;
        public string ContratoDv { get; set; } = string.Empty;
        public string TipoDesembolso { get; set; } = string.Empty; // "normal" | "adiantamento"
        public decimal ValorFgts { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Responsavel { get; set; } = string.Empty; // matrícula: quem ficou responsável pela baixa (atribuído na aprovação)
        public string Gestor { get; set; } = string.Empty;
        public string? Baixa { get; set; } // matrícula de quem confirmou a baixa; null = aguardando
    }
}
