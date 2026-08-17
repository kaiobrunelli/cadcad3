namespace PlataformaNotificacao.Domain.Enum
{
    // Ordem alinhada com Plataforma.UI.Shared.Enum.TipoNotificacao — os dois
    // precisam bater porque o valor numérico do enum viaja como está no
    // payload SignalR (ver SignalRNotificacaoService.HandlerObserver).
    public enum TipoNotificacao
    {
        Normal,
        Alerta,
        Urgente,
    }
}
