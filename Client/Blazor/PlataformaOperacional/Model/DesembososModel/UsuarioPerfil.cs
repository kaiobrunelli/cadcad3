using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.DesembososModel;

public class UsuarioPerfil
{
    [JsonPropertyName("usuario")]
    public string? Usuario { get; set; }
    [JsonPropertyName("perfil")]
    public string? Perfil { get; set; }
}
