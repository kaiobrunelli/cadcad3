using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class SerializarItem
    {
        public SerializarItem(int idChecklist, int? resposta, string? observacao)
        {
            IdChecklist = idChecklist;
            Resposta = resposta;
            Observacao = observacao;
        }

        [JsonPropertyName("idChecklist")]
        public int IdChecklist { get; set; } = 0;

        [JsonPropertyName("resposta")]
        public int? Resposta { get; set; } = 0;

        [JsonPropertyName("observacao")]
        public string? Observacao { get; set; } = string.Empty;
    }
}
