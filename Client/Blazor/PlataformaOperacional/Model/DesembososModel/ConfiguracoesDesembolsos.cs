using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.DesembososModel;

public class ConfiguracoesDesembolsos
{
    [JsonPropertyName("UnidadeMovimento")]
    public string UnidadeMovimento { get; set; } = string.Empty;
    [JsonPropertyName("UnidadeMovimentoDv")]
    public string UnidadeMovimentoDv { get; set; } = string.Empty;
    [JsonPropertyName("ImpressoraVirtualRedeCaixa")]
    public string ImpressoraVirtualRedeCaixa { get; set; } = string.Empty;
    [JsonPropertyName("ImpressoraVirtualServer")]
    public string ImpressoraVirtualServer { get; set; } = string.Empty;
    [JsonPropertyName("DestinoArquivo")]
    public string DestinoArquivo { get; set; } = string.Empty;
    [JsonPropertyName("Historico")]
    public string Historico { get; set; } = string.Empty;
}
