using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.DesembososModel;

public class ResumoCards
{
    [JsonPropertyName("tipoMovimentacao")]
    public string TipoMovimentacao { get; set; } = string.Empty;
    [JsonPropertyName("qtEntradas")]
    public long QtEntradas { get; set; }
    [JsonPropertyName("vrTotal")]
    public decimal VrTotal { get; set; }
    [JsonPropertyName("qtTotalEntradas")]
    public long QtTotalEntradas { get; set; }
}
