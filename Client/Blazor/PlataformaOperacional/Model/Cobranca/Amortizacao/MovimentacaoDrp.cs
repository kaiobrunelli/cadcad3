using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class MovimentacaoDrp : Movimentacao
	{
      



        [JsonPropertyName("idDrp")]
        public int IdDrp { get; set; }

        [JsonPropertyName("gifug")]
        public string? Gifug { get; set; }

        [JsonPropertyName("gifugDv")]
        public string? GifugDv { get; set; }

        [JsonPropertyName("tomador")]
        public string? Tomador { get; set; }

        [JsonPropertyName("nuDrp")]
        public string? NuDrp { get; set; }

        [JsonPropertyName("dvDrp")]
        public string? DvDrp { get; set; }

        [JsonPropertyName("senha")]
        public string? Senha { get; set; }

        [JsonPropertyName("qtdMovimentacoes")]
        public int? QtdMovimentacoes { get; set; }

        [JsonPropertyName("valor")]
        public decimal? Valor { get; set; }

        [JsonPropertyName("deObservacoes")]
        public string? DeObservacoes { get; set; }

   

        //[JsonPropertyName("idDrp")]
        //public int IdDrp { get; set; }

        //[JsonPropertyName("coControle")]
        //public int CoControle { get; set; }

        //[JsonPropertyName("dtLimQuitacao")]
        //public DateTime DtLimQuitacao { get; set; }

        //[JsonPropertyName("tomador")]
        //public string Tomador { get; set; } = "";

        //[JsonPropertyName("tomadorDv")]
        //public string TomadorDv { get; set; } = "";

        //[JsonPropertyName("gifug")]
        //public string Gifug { get; set; } = "";

        //[JsonPropertyName("gifugDv")]
        //public string GifugDv { get; set; } = "";

        //[JsonPropertyName("nuDrp")]
        //public int? NuDrp { get; set; }

        //[JsonPropertyName("dvDrp")]
        //public int? DvDrp { get; set; }

        //[JsonPropertyName("senha")]
        //public string Senha { get; set; } = "";

        //[JsonPropertyName("unidMov")]
        //public string UnidMov { get; set; } = "";

        //[JsonPropertyName("unidMovDv")]
        //public string UnidMovDv { get; set; } = "";

        //[JsonPropertyName("qtdMovimentacoes")]
        //public int QtdMovimentacoes { get; set; }

        //[JsonPropertyName("valor")]
        //public decimal Valor { get; set; }

        //[JsonPropertyName("icStatus")]
        //public int IcStatus { get; set; }

        //[JsonPropertyName("deObservacoes")]
        //public string DeObservacoes { get; set; } = "";



        //[JsonPropertyName("gifug")]
        //public string Gifug { get; set; } = "";	

        //[JsonPropertyName("tomador")]
        //public string Tomador { get; set; } = "";

        //[JsonPropertyName("nuDrp")]
        //public int? NuDrp { get; set; }

        //[JsonPropertyName("dvDrp")]
        //public string? DvDrp { get; set; }

        //[JsonPropertyName("unidMov")]
        //public string UnidMov { get; set; } = "";

        //[JsonPropertyName("qtdMovimentacoes")]
        //public int QtdMovimentacoes { get; set; }

        //[JsonPropertyName("valor")]
        //public decimal Valor { get; set; }

    }

}
