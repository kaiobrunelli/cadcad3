using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.CentralPermissoes
{
	public class Aplicativo
	{
		[JsonPropertyName("id")]
		public int ID { get; set; }
		[JsonPropertyName("deAplicativo")]
		public string DeAplicativo { get; set; } = string.Empty;
		[JsonPropertyName("sigla")]
		public string Sigla { get; set; } = string.Empty;
		[JsonPropertyName("iconAplicativo")]
		public string iconAplicativo { get; set; } = string.Empty;
		[JsonPropertyName("idArea")]
		public int IdArea { get; set; }
		[JsonPropertyName("nomeCentralPermissoes")]
		public string NomeCentralPermissoes { get; set; } = string.Empty;
		[JsonPropertyName("href")]
		public string Href { get; set; } = string.Empty;
	}
}
