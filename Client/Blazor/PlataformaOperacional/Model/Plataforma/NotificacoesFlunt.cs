using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Plataforma;

public class NotificacoesFlunt
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
