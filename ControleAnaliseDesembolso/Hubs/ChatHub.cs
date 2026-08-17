using Microsoft.AspNetCore.SignalR;

namespace ControleAnaliseDesembolso.Hubs;

// Hub próprio, local — o front conecta com ?userId={matricula} na URL (ver
// SignalRServiceUI.IniciarHubConnection). Cada conexão entra num grupo com o
// nome da própria matrícula, e é isso que permite mandar notificação só pra
// quem deve receber via Clients.Groups(destinatarios).
public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
        if (!string.IsNullOrWhiteSpace(userId))
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
        if (!string.IsNullOrWhiteSpace(userId))
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);

        await base.OnDisconnectedAsync(exception);
    }
}
