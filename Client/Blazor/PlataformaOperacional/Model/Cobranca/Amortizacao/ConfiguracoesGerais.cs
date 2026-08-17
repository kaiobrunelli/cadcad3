namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	using System.Text.Json.Serialization;

	public class ConfiguracoesGerais
	{
		[JsonPropertyName("unidadeMovimento")]
		public string UnidadeMovimento { get; set; } = "";

		[JsonPropertyName("unidadeMovimentoDv")]
		public string UnidadeMovimentoDv { get; set; } = "";

		[JsonPropertyName("qtdMaximaPorDrp")]
		public int QtdMaximaPorDrp { get; set; }

		[JsonPropertyName("contratoCci")]
		public string ContratoCci { get; set; } = "";

		[JsonPropertyName("contratoCciDv")]
		public string ContratoCciDv { get; set; } = "";

		[JsonPropertyName("impressoraVirutalRedeCaixa")]
		public string ImpressoraVirutalRedeCaixa { get; set; } = "";

		[JsonPropertyName("impressoraVirtualServer")]
		public string ImpressoraVirtualServer { get; set; } = "";

		[JsonPropertyName("destinoArquivo")]
		public string DestinoArquivo { get; set; } = "";
	}
}
