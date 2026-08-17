using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Aplicacao.Preditor
{
	public class ReadDestinatarioEmail
	{
		
		[JsonPropertyName("coDestinatario")]
		public int CoDestinatario { get; set; }
		[JsonPropertyName("deDestinatario")]
		public string? DeDestinatario { get; set; }
		[JsonPropertyName("tipoDestinatario")]
		public string? TipoDestinatario { get; set; }
		[JsonPropertyName("emailDestinatario")]
		public string? EmailDestinatario { get; set; }
	}
}
