using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class ContratacaoCadastrarRessalva
    {

        [JsonPropertyName("idChecklist")] public int? IdChecklist { get; set; }
        [JsonPropertyName("idChecklistVerificacao")] public int? IdChecklistVerificacao { get; set; }
        [JsonPropertyName("observacao")] public string? Observacao { get; set; } = "";
        [JsonPropertyName("contrato")] public string Contratao { get; set; } = "";
        [JsonPropertyName("idCategoriaObs")] public int? IdCategoriaObs { get; set; }

    }
}
