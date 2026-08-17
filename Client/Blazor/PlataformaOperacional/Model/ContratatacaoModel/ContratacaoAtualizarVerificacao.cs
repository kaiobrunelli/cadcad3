using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class ContratacaoAtualizarVerificacao
    {     

        [JsonPropertyName("idChecklist")]
        public int? IdChecklist { get; set; } = 0;

        [JsonPropertyName("idResposta")]
        public int? Resposta { get; set; } = 0;

        //[JsonPropertyName("observacao")]
        //public string? Observacao { get; set; } = string.Empty; 

       

    }
}
