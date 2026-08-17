using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.AplicacaoModel
{
	public class SituacaoContrato
	{
        [JsonPropertyName("coRegistro")] public int CoRegistro { get; set; }
		[JsonPropertyName("coOperacao")] public string CoOperacao { get; set; } = string.Empty;
		[JsonPropertyName("coDvOperacao")] public int CoDvOperacao { get; set; }
		[JsonPropertyName("situacao")] public string Situacao { get; set; } = string.Empty;
		[JsonPropertyName("observacao")] public string  Observacao { get; set; } = string.Empty;
	}
}
