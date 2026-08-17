using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class ConjuntoTo
	{

		[JsonPropertyName("vrTo01")]
		public decimal VrTo01 { get; set; } = 0;
		[JsonPropertyName("vrTo02")]
		public decimal VrTo02 { get; set; } = 0;
		[JsonPropertyName("vrTo03")]
		public decimal VrTo03 { get; set; } = 0;
		[JsonPropertyName("vrTo04")]
		public decimal VrTo04 { get; set; } = 0;
		[JsonPropertyName("vrTo05")]
		public decimal VrTo05 { get; set; } = 0;
		[JsonPropertyName("vrTo06")]
		public decimal VrTo06 { get; set; } = 0;
		[JsonPropertyName("vrTo07")]
		public decimal VrTo07 { get; set; } = 0;
		[JsonPropertyName("vrTo08")]
		public decimal VrTo08 { get; set; } = 0;
		[JsonPropertyName("vrTo09")]
		public decimal VrTo09 { get; set; } = 0;
		[JsonPropertyName("vrTo010")]
		public decimal VrTo010 { get; set; } = 0;

		public ConjuntoTo() { }

		public ConjuntoTo(decimal v1, decimal v2, decimal v3, decimal v4, decimal v5, decimal v6, decimal v7)
		{
			VrTo01 = v1;
			VrTo02 = v2;
			VrTo03 = v3;
			VrTo04 = v4;
			VrTo05 = v5;
			VrTo06 = v6;
			VrTo07 = v7;
		}
	}
}