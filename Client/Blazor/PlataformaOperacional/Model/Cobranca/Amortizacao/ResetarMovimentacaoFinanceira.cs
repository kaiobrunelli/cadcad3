using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class ResetarMovimentacaoFinanceira
	{
		[JsonPropertyName("coMovimentacaoFinanceira")]
		public int CoMovimentacaoFinanceira { get; set; }

		[JsonPropertyName("acaoUsuario")]
		public string AcaoUsuario { get; set; } = string.Empty;

		[JsonPropertyName("tiposDeOperacao")]
		public TiposDeOperacao TiposDeOperacao { get; set; } = new();
		
	}

}
