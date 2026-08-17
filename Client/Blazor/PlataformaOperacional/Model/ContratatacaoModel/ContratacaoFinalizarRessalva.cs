using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class ContratacaoFinalizarRessalva
    {
        [JsonPropertyName("idRessalva")]
        public int IdRessalva { get; set; }
        [JsonPropertyName("tratado")]
        public bool Tratado { get; set; }
        [JsonPropertyName("observacaoTratamento")] 
        public string? ObservacaoTratamento { get; set; }

    }
}
