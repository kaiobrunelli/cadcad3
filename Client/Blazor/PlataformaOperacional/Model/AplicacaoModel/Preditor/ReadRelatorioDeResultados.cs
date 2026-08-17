namespace PlataformaOperacional.Model.AplicacaoModel.Preditor
{
    public class ReadRelatorioDeResultados
    {
        public DateTime? DtPredicao { get; set; }

        public decimal? PubCalculado { get; set; }
        public decimal? PubEfetivado { get; set; }
        public decimal? PubRealizado { get; set; }
        public decimal? PubResultado { get; set; }
        public decimal? PriCalculado { get; set; }
        public decimal? PriEfetivado { get; set; }
        public decimal? PriResultado { get; set; }
        public decimal? PriRealizado { get; set; }
        public decimal? TotalCalculado { get; set; }
        public decimal? TotalEfetivado { get; set; }
        public decimal? TotalRealizado { get; set; }
        public decimal? TotalResultado { get; set; }

        public int? IcStatus { get; set; }
    }
}
