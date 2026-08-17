using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class ContratacaoConsultarResumoGeralChecklist
    {
        [JsonPropertyName("totalContratosConcluidos")]
        public int? totalContratosConcluidos { get; set; } 
        [JsonPropertyName("totalIrregulares")]
        public int? totalIrregulares { get; set; } 
        [JsonPropertyName("totalContratosVerificados")]
        public int? totalContratosVerificados { get; set; } 
    }
}
