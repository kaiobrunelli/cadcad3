using PlataformaOperacional.Model.CentralPermissoes;
using PlataformaOperacionalV1.Model.CentralPermissoes;
using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Usuario
{
    public class ConfiguracoesUsuario
    {
		[JsonPropertyName("matricula")]
		public string Matricula { get; set; } = "";

		[JsonPropertyName("nome")]
		public string NomeCompleto { get; set; } = "";

		[JsonPropertyName("unidade")]
		public int Unidade { get; set; }

		[JsonPropertyName("areas")]
		public List<Area> Areas { get; set; } = new List<Area>();

	}
}
