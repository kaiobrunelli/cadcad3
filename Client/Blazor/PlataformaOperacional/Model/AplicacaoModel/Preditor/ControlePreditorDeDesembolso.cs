using PlataformaOperacional.Model.Cobranca;
using PlataformaOperacional.Model.Plataforma;
using System.Text.Json.Serialization;


namespace PlataformaOperacional.Model.Aplicacao.Preditor
{
	public class ControlePreditorDeDesembolso
	{
		[JsonPropertyName("dtPredicao")]
		public DateTime? DtPredicao { get; set; }

		[JsonPropertyName("pubResponsavel")]
		public string? PubResponsavel { get; set; }

		[JsonPropertyName("pubConfirmacao")]
		public DateTime? PubConfirmacao { get; set; }

		[JsonPropertyName("analiticoSetorPublico")]
		public AnaliticoSetorPublico? AnaliticoSetorPublico { get; set; }

		[JsonPropertyName("priResponsavel")]
		public string? PriResponsavel { get; set; }

		[JsonPropertyName("priConfirmacao")]
		public DateTime? PriConfirmacao { get; set; }

		[JsonPropertyName("analiticoSetorPrivado")]
		public AnaliticoSetorPrivado? AnaliticoSetorPrivado { get; set; }

		[JsonPropertyName("finalizacaoResponsavel")]
		public string? FinalizacaoResponsavel { get; set; }

		[JsonPropertyName("emailConfirmacao")]
		public string? EmailConfirmacao { get; set; }

		[JsonPropertyName("icStatus")]
		public string? IcStatus { get; set; }

		[JsonPropertyName("btnPrivado")]
		public bool BtnPrivado { get; set; }

		[JsonPropertyName("btnPublico")]
		public bool BtnPublico { get; set; }
		
		[JsonPropertyName("btnFinalizador")]
		public bool BtnFinalizador { get; set; }

		[JsonPropertyName("btnConfigurar")]
		public bool BtnConfigurar { get; set; }

		[JsonPropertyName("alertas")]
		public List<PlataformaAlerta>? Alertas { get; set; }
	}

	public class AnaliticoSetorPrivado
	{
		[JsonPropertyName("vrLegado")]
		public decimal? ValorLegado { get; set; }
		[JsonPropertyName("vrProCotista")]
		public decimal? ValorProCotista { get; set; }
		[JsonPropertyName("vrConsolidado")]
		public decimal? ValorConsolidado { get; set; }
		[JsonPropertyName("vrCalculado")]
		public decimal? ValorCalculado { get; set; }
		[JsonPropertyName("vrTotal")]
		public decimal? ValorTotal { get; set; }

	}

	public class AnaliticoSetorPublico
	{
		[JsonPropertyName("vrAFCaixa")]
		public decimal? VrAFCaixa { get; set; }
		[JsonPropertyName("vrOutrosAF")]
		public decimal? VrOutrosAF { get; set; }
        [JsonPropertyName("vrCalculado")]
        public decimal? ValorCalculado { get; set; }
        [JsonPropertyName("vrTotal")]
		public decimal? ValorTotal { get; set; }

	}


}
