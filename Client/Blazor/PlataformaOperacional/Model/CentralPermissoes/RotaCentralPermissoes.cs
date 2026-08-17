using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.CentralPermissoes
{
    public class RotaCentralPermissoes
    {
        [JsonPropertyName("tipoDeProjeto")]
        public string TipoDeProjeto { get; set; } = "";
		[JsonPropertyName("httpMethod")]
        public string HttpMethod { get; set; } = "";
		[JsonPropertyName("caminho")]
        public string Caminho { get; set; } = "";
	}
}
