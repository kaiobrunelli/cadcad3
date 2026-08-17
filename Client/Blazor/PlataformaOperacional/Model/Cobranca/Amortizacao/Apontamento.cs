using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class Apontamento
	{

		[JsonPropertyName("coApontamento")]
		public int CoApontamento { get; set; }

		[JsonPropertyName("dtCarga")]
		public DateTime DtCarga { get; set; }

		[JsonPropertyName("coControle")]
		public int CoControle { get; set; }

		[JsonPropertyName("coUnidade")]
		public string CoUnidade { get; set; } = "";

		[JsonPropertyName("nuEmpreendimento")]
		public string NuEmpreendimento { get; set; } = "";

		[JsonPropertyName("nuDigitoEmpreendimento")]
		public string NuDigitoEmpreendimento { get; set; } = "";

		[JsonPropertyName("nuOperacao")]
		public string NuOperacao { get; set; } = "";

		[JsonPropertyName("nuOperacaoDv")]
		public string NuOperacaoDv { get; set; } = "";

		[JsonPropertyName("noTipoMovimentacao")]
		public string NoTipoMovimentacao { get; set; } = "";

		[JsonPropertyName("nuLinha")]
		public string NuLinha { get; set; } = "";

		[JsonPropertyName("nuOr")]
		public string NuOr { get; set; } = "";

		[JsonPropertyName("nuTipoOperacao")]
		public string NuTipoOperacao { get; set; } = "";

		[JsonPropertyName("aaOrcamento")]
		public int AaOrcamento { get; set; }

		[JsonPropertyName("mutFin")]
		public string MutFin { get; set; } = "";

		[JsonPropertyName("pmcmv")]
		public string Pmcmv { get; set; } = "";

		[JsonPropertyName("dtCredito")]
		public DateTime DtCredito { get; set; }

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

		[JsonPropertyName("vrRepassado")]
		public decimal VrRepassado { get; set; }

		[JsonPropertyName("icStatus")]
		public int IcStatus { get; set; }

		[JsonPropertyName("deObservacoes")]
		public string DeObservacoes { get; set; } = "";

		[JsonPropertyName("coAmortizacao")]
		public int CoAmortizacao { get; set; }

	}
}
