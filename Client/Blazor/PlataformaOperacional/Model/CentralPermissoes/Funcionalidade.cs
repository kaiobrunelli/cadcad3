using PlataformaOperacional.Model.CentralPermissoes;
using System.Text.Json.Serialization;

namespace PlataformaOperacionalV1.Model.CentralPermissoes
{
    public class Funcionalidade
    {
        [JsonPropertyName("nome")]
        public string NomeDaFuncionalidade { get; set; } = "";
        [JsonPropertyName("rotas")]
        public List<RotaCentralPermissoes> ListadeRotas { get; set; } = new List<RotaCentralPermissoes>();
    }
}
