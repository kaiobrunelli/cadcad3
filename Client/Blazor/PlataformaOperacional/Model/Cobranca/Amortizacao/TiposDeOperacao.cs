using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class TiposDeOperacao
	{
		[JsonPropertyName("vrTo01")]
		public decimal VrTo01 { get; set; }

		[JsonPropertyName("vrTo02")]
		public decimal VrTo02 { get; set; }

		[JsonPropertyName("vrTo03")]
		public decimal VrTo03 { get; set; }

		[JsonPropertyName("vrTo04")]
		public decimal VrTo04 { get; set; }

		[JsonPropertyName("vrTo05")]
		public decimal VrTo05 { get; set; }

		[JsonPropertyName("vrTo06")]
		public decimal VrTo06 { get; set; }

		[JsonPropertyName("vrTo07")]
		public decimal VrTo07 { get; set; }

		[JsonPropertyName("vrTo08")]
		public decimal VrTo08 { get; set; }

		[JsonPropertyName("vrTo09")]
		public decimal VrTo09 { get; set; }

		[JsonPropertyName("vrTo010")]
		public decimal VrTo010 { get; set; }
	}

}

