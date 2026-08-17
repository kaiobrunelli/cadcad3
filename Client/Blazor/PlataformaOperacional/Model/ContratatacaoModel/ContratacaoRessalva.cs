using System.Data;
using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class ContratacaoRessalva
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("observacao")] public string? Observacao { get; set; } = "";
        [JsonPropertyName("contrato")] public string Contratao { get; set; } = "";
        [JsonPropertyName("idChecklist")] public int? IdChecklist { get; set; }
        [JsonPropertyName("idChecklistVerificacao")] public int? IdChecklistVerificacao { get; set; }
        [JsonPropertyName("idCategoriaObs")] public int? IdCategoriaObs { get; set; }
        [JsonPropertyName("usuarioInclusao")] public string? UsuarioInclusao { get; set; } = "";
        [JsonPropertyName("dataInclusao")] public DateTime DataInclusao { get; set; }
        [JsonPropertyName("usuarioConclusao")] public string? UsuarioConclusao { get; set; } = "";
        [JsonPropertyName("dataConclusao")] public DateTime? DataConclusao { get; set; }
        [JsonPropertyName("tratado")] public bool Tratado { get; set; } = false;
        [JsonPropertyName("observacaoTratamento")] public string? ObservacaoTratamento { get; set; }
    }
}
