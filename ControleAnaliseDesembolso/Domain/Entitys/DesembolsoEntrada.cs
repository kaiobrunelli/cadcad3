namespace ControleAnaliseDesembolso.Domain.Entitys
{
    public class DesembolsoEntrada
    {
        public int Id { get; set; }

        public int IdFpdGegov { get; set; }

        public string? CoGigov { get; set; }

        public int CoFpd { get; set; }

        public string? DeTomador { get; set; }

        public string? CoTomador { get; set; }

        public string? DeOperacional { get; set; }

        public string? CoOperacional { get; set; }

        public string? DeAgentePromotor { get; set; }

        public string? CoAgentePromotor { get; set; }

        public string? ContratoAf { get; set; }

        public string? DvAf { get; set; }

        public string? ContratoAo { get; set; }

        public DateTime? DtEngenharia { get; set; }

        public string? SituacaoObra { get; set; }

        public float? PercentualObra { get; set; }
    }
}
