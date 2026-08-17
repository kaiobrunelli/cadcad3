using System.Text.Json.Serialization;

namespace PlataformaOperacionalV1.Model.CentralPermissoes
{
    public class Modulo
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = "";
        [JsonPropertyName("href")]
        public string Href { get; set; } = "";
      
    }
}
