using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class ContratacaoConsultarResumoTodosContratos
    {
        [JsonPropertyName("coTomador")]
        public string CoTomador {  get; set; }

        [JsonPropertyName("contrato")]
        public string Contrato { get; set; } = "";

        [JsonPropertyName("dtAssinatura")]
        public DateTime DataAssinatura { get; set; }

        [JsonPropertyName("dataSolicitacao")]
        public DateTime DataSolicitacao { get; set; }

        [JsonPropertyName("totalVerificacoes")]
        public int TotalVerificacoes {  get; set; } 

        [JsonPropertyName("concluidos")]
        public int Concluidos {  get; set; } 

        [JsonPropertyName("irregular")]
        public int Irregular {  get; set; }

        [JsonPropertyName("situacao")]
        public string Situacao { get; set; } = "";

        [JsonPropertyName("podeFinalizar")]
        public bool PodeFinalizar { get; set; } = false;
    }
}
