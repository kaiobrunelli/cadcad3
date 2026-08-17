using Microsoft.Extensions.Primitives;
using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratacaoModel
{
    public class ContratacaoVerificacao
    {
        [JsonPropertyName("idChecklist")]
        public int? IdChecklist { get; set; } = 0;
  
        [JsonPropertyName("idChecklistVerificacao")]
        public int? IdChecklistVerificacao { get; set; } 

        [JsonPropertyName("idVerificacao")]
        public int IdVerificacao { get; set; }
        [JsonPropertyName("deVerificacao")]
        public string? DeVerificacao { get; set; } = "";
        [JsonPropertyName("dtAnalise")]
        public DateTime? DtAnalise { get; set; }
        [JsonPropertyName("usuario")]
        public string? Usuario { get; set; } = "";
        [JsonPropertyName("resposta")]
        public int? Resposta { get; set; } = 0;
        [JsonPropertyName("temObs")]
        public bool TemObs { get; set; }
        [JsonPropertyName("observacao")]
        public string? Observacao { get; set; } = "";
		[JsonPropertyName("temRessalva")]
		public bool TemRessalva { get; set; }
	}
}
