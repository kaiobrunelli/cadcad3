using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class ControleGeral
	{
		[JsonPropertyName("coControle")]
		public int CoControle { get; set; }

		[JsonPropertyName("dtReferencia")]
		public DateTime DtReferencia { get; set; }

		[JsonPropertyName("dtCargaIniciada")]
		public DateTime? DtCargaIniciada { get; set; }

		[JsonPropertyName("qtdTotalApontamentos")]
		public int? QtdTotalApontamentos { get; set; }

		[JsonPropertyName("vlTotalApontamentos")]
		public decimal? VlTotalApontamentos { get; set; }

		[JsonPropertyName("qtdAmortizacoes")]
		public int? QtdAmortizacoes { get; set; }

		[JsonPropertyName("vlAmortizacoes")]
		public decimal? VlAmortizacoes { get; set; }

		[JsonPropertyName("qtdDevolucoes")]
		public int? QtdDevolucoes { get; set; }

		[JsonPropertyName("vlDevolucoes")]
		public decimal? VlDevolucoes { get; set; }

		[JsonPropertyName("qtdCci")]
		public int? QtdCci { get; set; }

		[JsonPropertyName("vlCci")]
		public decimal? VlCci { get; set; }

		[JsonPropertyName("qtdDrp")]
		public int? QtdDrp { get; set; }

		[JsonPropertyName("vlDrp")]
		public decimal? VlDrp { get; set; }

		[JsonPropertyName("qtdAnalisar")]
		public int QtdAnalisar { get; set; }

		[JsonPropertyName("responsavelEmitirDrp")]
		public string? ResponsavelEmitirDrp { get; set; }

		[JsonPropertyName("dtFimEmitirDrp")]
		public DateTime? DtFimEmitirDrp { get; set; }

		[JsonPropertyName("responsavelBaixarDrp")]
		public string? ResponsavelBaixarDrp { get; set; }

		[JsonPropertyName("dtFimBaixarDrp")]
		public DateTime? DtFimBaixarDrp { get; set; }

		[JsonPropertyName("situacao")]
		public string Situacao { get; set; } = string.Empty;
	}
}