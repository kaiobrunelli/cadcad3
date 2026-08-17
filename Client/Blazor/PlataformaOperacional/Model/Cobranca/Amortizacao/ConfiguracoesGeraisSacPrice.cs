namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	using System.Text.Json.Serialization;

	public class ConfiguracoesGeraisSacPrice
	{
		[JsonPropertyName("unidadeMovimento")]
		public string UnidadeMovimento { get; set; } = "";

		[JsonPropertyName("unidadeMovimentoDv")]
		public string UnidadeMovimentoDv { get; set; } = "";

		[JsonPropertyName("qtdMaximaPorDrp")]
		public int QtdMaximaPorDrp { get; set; }

		[JsonPropertyName("contratoCciSac")]
		public string ContratoCciSac { get; set; } = "";

		[JsonPropertyName("contratoCciSacDv")]
		public string ContratoCciSacDv { get; set; } = "";

		[JsonPropertyName("percentualSac")]
		public decimal PercentualSac { get; set; }

		[JsonPropertyName("contratoCciPrice")]
		public string ContratoCciPrice { get; set; } = "";

		[JsonPropertyName("contratoCciPriceDv")]
		public string ContratoCciPriceDv { get; set; } = "";

		[JsonPropertyName("percentualPrice")]
		public decimal PercentualPrice { get; set; }

		[JsonPropertyName("impressoraVirutalRedeCaixa")]
		public string ImpressoraVirutalRedeCaixa { get; set; } = "";

		[JsonPropertyName("impressoraVirtualServer")]
		public string ImpressoraVirtualServer { get; set; } = "";

		[JsonPropertyName("destinoArquivo")]
		public string DestinoArquivo { get; set; } = "";
	}
}
