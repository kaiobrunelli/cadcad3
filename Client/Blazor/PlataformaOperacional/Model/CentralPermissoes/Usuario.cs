using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.CentralPermissoes
{
	public class Usuario
	{
		[JsonPropertyName("matricula")]
		public string Matricula { get; set; } = "";

		[JsonPropertyName("unidade")]
		public int Unidade { get; set; } 

        [JsonPropertyName("enderecoLogico")]
		public string EnderecoLogico { get; set; } = "";
	}
}
