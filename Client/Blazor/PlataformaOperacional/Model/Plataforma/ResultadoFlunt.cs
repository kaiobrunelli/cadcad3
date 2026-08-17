using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Plataforma;

public class ResultadoFlunt
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("notifications")]
    public List<NotificacoesFlunt> Notifications { get; set; } = new();
}
