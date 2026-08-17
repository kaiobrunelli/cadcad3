using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.AplicacaoModel
{
    public class SaldoResidualContrato
    {
		[JsonPropertyName("coRegistro")]
		public int CoRegistro { get; set; }
		[JsonPropertyName("idPedido")]
        public int? IdPedido { get; set; }
        public string? CoOrigemRecurso { get; set; }
        public string CoUgc { get; set; } = "";
        public string? AaAutOrc { get; set; }
        public string? CoContaOrc { get; set; }
        public string CoUf { get; set; } = "";
        public string CoTomador { get; set; } = "";
        public string CoLinha { get; set; } = "";
        public string CoObjetivo { get; set; } = "";
        public string? CoSitContrato { get; set; }
        public string? CoSitCobranca { get; set; }
        [JsonPropertyName("coOperacao")]
        public string CoOperacao { get; set; } = "";
        [JsonPropertyName("coDvOperacao")]
        public string CoDvOperacao { get; set; } = "";
        public DateTime? DtAssinatura { get; set; }
        public double VrEmprestimo { get; set; }
        public decimal VrLiberado { get; set; }
        public decimal VrSaldoCredor { get; set; }
        public double VrSaldoResidual { get; set; }
        public double? VrSaldoResidualProcessado { get; set; }
        public string DtAnoMes { get; set; } = "";
        public string CoSitObra { get; set; } = "";
        public Single PcRealizado { get; set; }
        public double VrSaldoTratar { get; set; }
        public double? VrSaldoTratado { get; set; }
        public DateTime? DtTratamento { get; set; }
        public DateTime? DtPosBase { get; set; }
        public DateTime? DtRelatorio { get; set; }
        public DateTime? DtProcessamento { get; set; }
        public string? MatriculaOperador { get; set; }
        public Int16? IcProcessamento { get; set; }
        [JsonPropertyName("deObservacao")]
        public string? DeObservacao { get; set; }
        public double? VrUltimoDesemb { get; set; }
    }
}
