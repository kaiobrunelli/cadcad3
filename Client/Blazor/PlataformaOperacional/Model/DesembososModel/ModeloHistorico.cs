using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.DesembososModel;

public class ModeloHistorico
{
    [JsonPropertyName("IdModelo")]
    public int IdModelo { get; set; }
    [JsonPropertyName("Processo")]
    public string Processo { get; set; } = null!;
    [JsonPropertyName("Tipo")]
    public string Tipo { get; set; } = null!;
    [JsonPropertyName("Tm")]
    public string Tm { get; set; } = null!;
    [JsonPropertyName("ModeloHistoricoTexto")]
    public string ModeloHistoricoTexto { get; set; } = null!;
    [JsonPropertyName("DtInclusao")]
    public DateTime DtInclusao { get; set; }
    [JsonPropertyName("DtExclusao")]
    public DateTime? DtExclusao { get; set; }
}
