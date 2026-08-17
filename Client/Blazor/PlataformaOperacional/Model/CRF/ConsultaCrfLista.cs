using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.CRF
{
	public class ConsultaCrfLista
	{
        [JsonPropertyName("dtPosicao")]    
        public DateTime DtPosicao { get; set; }
        [JsonPropertyName("coTomador")]
        public string CoTomador { get; set; } = string.Empty;
        [JsonPropertyName("noTomador")]
        public string NoTomador { get; set; } = string.Empty;
        [JsonPropertyName("coCgc")]
        public string CoCgc { get; set; } = string.Empty;
        [JsonPropertyName("icRegularidade")]
        public string? IcRegularidade { get; set; }
        [JsonPropertyName("icVigencia")]
        public string? IcVigencia { get; set; }
        [JsonPropertyName("dtVigencia")]
        public DateTime? DtVigencia { get; set; }
        [JsonPropertyName("nuCrf")]
        public string? NuCrf { get; set; }
    }
}
