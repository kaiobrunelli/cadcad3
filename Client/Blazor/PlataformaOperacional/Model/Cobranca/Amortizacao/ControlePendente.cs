using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
    public class ControlePendente
    {
		[JsonPropertyName("coControle")]
		public int CoControle { get; set; }

		[JsonPropertyName("dtReferencia")]
		public DateTime DtReferencia { get; set; }

		[JsonPropertyName("dtRecebimentoArquivo")]
		public DateTime? DtRecebimentoArquivo { get; set; }
	}
}
