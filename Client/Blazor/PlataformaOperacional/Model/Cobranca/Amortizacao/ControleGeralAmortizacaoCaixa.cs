using MudBlazor;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
    public class ControleGeralAmortizacaoCaixa
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
        public string Situacao { get; set; } = "";

        [JsonPropertyName("alertas")]
        public List<AmortizacaoCaixaAlerta> Alertas { get; set; } = new();

		[JsonPropertyName("botaoExecutar")]
		public bool BotaoExecutar { get; set; }

        [JsonPropertyName("descBotaoExecutar")]
        public string DescBotaoExecutar { get; set; } = "";

		[JsonPropertyName("botaoConfigurar")]
		public bool BotaoConfigurar { get; set; } 

		[JsonPropertyName("botaoCancelar")]
		public bool BotaoCancelar { get; set; }

		[JsonPropertyName("descBotaoCancelar")]
		public string DescBotaoCancelar { get; set; }

		[JsonPropertyName("locationSignalR")]
        public string locationSignalR { get; set; } = "";

	



		public static List<ControleGeralAmortizacaoCaixa> CriarLista(int quantidade)
        {
            var lista = new List<ControleGeralAmortizacaoCaixa>();
            var random = new Random();
            var dtReferencia = new DateTime(2025, 9, 9);

            for (int i = 0; i < quantidade; i++)
            {
                var controle = new ControleGeralAmortizacaoCaixa
                {
                    CoControle = i + 1,
                    DtReferencia = dtReferencia,
                    DtCargaIniciada = dtReferencia.AddDays(1).AddHours(random.Next(0, 24)),
                    QtdTotalApontamentos = random.Next(1000, 2000),
                    VlTotalApontamentos = Convert.ToDecimal(random.Next(500_000, 2_500_000)),
					QtdAmortizacoes = random.Next(500, 1500),
                    VlAmortizacoes = Convert.ToDecimal(random.Next(500_000, 2_500_000)),
                    QtdDevolucoes = random.Next(100, 600),
                    VlDevolucoes = Convert.ToDecimal(random.Next(100_000, 1_500_000)),
					QtdCci = random.Next(1, 30),
                    VlCci = random.Next() * 2_000_000,
                    ResponsavelEmitirDrp = null,
                    DtFimEmitirDrp = null,
                    ResponsavelBaixarDrp = null,
                    DtFimBaixarDrp = null
                   
                };

                lista.Add(controle);
            }

            return lista;
        }

    }

}

