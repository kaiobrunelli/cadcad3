using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.CentralPermissoes
{
	public class SenhaUsuario
	{
		public SenhaUsuario(string senha)
		{
			Senha = senha;
			MyExtra = false;
		}


		[JsonPropertyName("senha")]
		public string Senha { get; set; } = "";

		[JsonPropertyName("myExtra")]
		public bool MyExtra { get; set; }

	}
}
