using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.MonitoramentoModel;

public class Entrada
{
    [JsonPropertyName("dataEfetiva")]
    public string? DataEfetiva { get; set; }
    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }
    [JsonPropertyName("historico")]
    public string? Historico { get; set; }
    [JsonPropertyName("codigoHistorico")]
    public int CodigoHistorico { get; set; }
    [JsonPropertyName("origem")]
    public string? Origem { get; set; }
    [JsonPropertyName("conta")]
    public int Conta { get; set; }
    [JsonPropertyName("DtApropriacao")]
    public string? DtApropriacao { get; set; }
    [JsonPropertyName("DtProcessamento")]
    public string? DtProcessamento { get; set; }
    [JsonPropertyName("Observacao")]
    public string? Observacao { get; set; }
}
