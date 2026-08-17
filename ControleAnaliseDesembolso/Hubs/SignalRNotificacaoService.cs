using Microsoft.AspNetCore.SignalR;
using PlataformaNotificacao.Domain;

namespace ControleAnaliseDesembolso.Hubs;

// Espelha o SignalRService real (RedeCaixaUtilitario.Application) — mesmo
// padrão de broadcast por grupo (Clients.Groups(destinatarios)), só que
// hospedado aqui no nosso ChatHub local em vez do hub externo da plataforma.
public class SignalRNotificacaoService
{
    private readonly IHubContext<ChatHub> _hubContext;

    public SignalRNotificacaoService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task HandlerObserver(object? sender, MensagemNotificacao e)
    {
        Console.WriteLine(
            $"Escopo={e.Escopo} | Qtd={e.Destinatarios?.Count ?? 0} | Destinatarios={string.Join(",", e.Destinatarios ?? [])}");

        if (e.Destinatarios == null || e.Destinatarios.Count == 0)
        {
            Console.WriteLine("ERRO: notificação sem destinatários");
        }

        var destino = e.Destinatarios is { Count: > 0 }
            ? _hubContext.Clients.Groups(e.Destinatarios)
            : (IClientProxy)_hubContext.Clients.All;

        // PlataformaNotificacao.Domain.Enum.TipoNotificacao/EscopoNotificacao
        // foram alinhados (mesma ordem/valor) com
        // Plataforma.UI.Shared.Enum.TipoNotificacao/EscopoNotificacao — não
        // precisa mais remapear por nome. ChaveConexao/Destinatarios têm
        // [JsonIgnore] em MensagemNotificacao, então não vazam no payload.
        await destino.SendAsync(e.ChaveConexao, e);
    }
}
