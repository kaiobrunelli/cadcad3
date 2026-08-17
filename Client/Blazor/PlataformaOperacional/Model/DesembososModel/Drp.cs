using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.DesembososModel;

public class Drp
{
    [JsonPropertyName("IdDrp")]
    public int IdDrp { get; set; }
    [JsonPropertyName("CoControle")]
    public int CoControle { get; set; }
    [JsonPropertyName("Gifug")]
    public string? Gifug { get; set; }
    [JsonPropertyName("IdModelo")]
    public string? IdModelo { get; set; }
    [JsonPropertyName("Tomador")]
    public string? Tomador { get; set; }
    [JsonPropertyName("NuDrp")]
    public string? NuDrp { get; set; }
    [JsonPropertyName("DvDrp")]
    public string? DvDrp { get; set; }
    [JsonPropertyName("Senha")]
    public string? Senha { get; set; }
    [JsonPropertyName("QtdMovimentacoes")]
    public int? QtdMovimentacoes { get; set; }
    [JsonPropertyName("Valor")]
    public decimal? Valor { get; set; }
    [JsonPropertyName("DeObservacoes")]
    public string? DeObservacoes { get; set; }
    [JsonPropertyName("Sispb")]
    public string? Sispb { get; set; }
    [JsonPropertyName("Situacao")]
    public string? Situacao { get; set; }
    [JsonPropertyName("DeSituacao")]
    public string? DeSituacao { get; set; }
}
