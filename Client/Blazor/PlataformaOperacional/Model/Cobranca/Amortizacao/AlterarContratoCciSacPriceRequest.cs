namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	using System.Text.Json.Serialization;

	public class AlterarContratoCciSacPriceRequest
	{
		[JsonPropertyName("nuContratoSac")]
		public string? NuContratoSac { get; set; }

		[JsonPropertyName("nuContratoSacDv")]
		public string? NuContratoSacDv { get; set; }

		[JsonPropertyName("percentualSac")]
		public decimal PercentualSac { get; set; }

		[JsonPropertyName("nuContratoPrice")]
		public string? NuContratoPrice { get; set; }

		[JsonPropertyName("nuContratoPriceDv")]
		public string? NuContratoPriceDv { get; set; }

		[JsonPropertyName("percentualPrice")]
		public decimal PercentualPrice { get; set; }
	}
}
