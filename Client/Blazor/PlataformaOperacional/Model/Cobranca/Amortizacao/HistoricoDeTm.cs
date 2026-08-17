using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class HistoricoDeTm
	{
		[JsonPropertyName("idModelo")]
		public int IdModelo { get; set; }

		[JsonPropertyName("processo")]
		public string Processo { get; set; } = "";


		[JsonPropertyName("tipo")]
		public string Tipo { get; set; } = "";


		[JsonPropertyName("tm")]
		public string Tm { get; set; } = "";


		[JsonPropertyName("modeloHistorico")]
		public string ModeloHistorico { get; set; } = "";

	}
}