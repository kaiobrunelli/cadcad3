using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class AlterarHistoricoTms
	{
		[JsonPropertyName("idModelo")]
		public int IdModelo { get; set; }

		[JsonPropertyName("historico")]
		public string Historico { get; set; } = "";

	}
}
