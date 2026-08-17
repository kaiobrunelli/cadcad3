using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.CentralPermissoes
{

	public class Area
	{
		[JsonPropertyName("id")]
		public int ID { get; set; }
		[JsonPropertyName("deArea")]
		public string DeArea { get; set; } = string.Empty;
		[JsonPropertyName("icon")]
		public string Icon { get; set; } = string.Empty;
		[JsonPropertyName("pageInicial")]
		public string? PageInicial { get; set; } = string.Empty;
		[JsonPropertyName("aplicativos")]
		public List<Aplicativo> Aplicativo { get; set; } = new List<Aplicativo>();
	}
}
