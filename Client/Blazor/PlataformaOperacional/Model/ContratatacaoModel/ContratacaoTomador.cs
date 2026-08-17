using PlataformaOperacional.Model.ContratacaoModel;
using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class ContratacaoTomador
    {
        [JsonPropertyName("coTomador")]
        public string CoTomador { get; set; } = "";
        [JsonPropertyName("deTomador")]
        public string DeTomador { get; set; } = "";
        [JsonPropertyName("totalContratos")]
        public int TotalContratos { get; set; }
        [JsonPropertyName("contratosConcluidos")]
        public int ContratosConcluidos { get; set; }
        [JsonPropertyName("contratosIrregulares")]
        public int ContratosIrregulares { get; set; }
        [JsonPropertyName("contratos")]
        public List<ContratacaoContrato> ListaContratos { get; set; } = new List<ContratacaoContrato>();
    }
}
