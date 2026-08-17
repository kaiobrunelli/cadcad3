namespace PlataformaNotificacao.Domain.Enum
{
    // Ordem alinhada com Plataforma.UI.Shared.Enum.EscopoNotificacao — os dois
    // precisam bater porque o valor numérico do enum viaja como está no
    // payload SignalR (ver SignalRNotificacaoService.HandlerObserver).
    public enum EscopoNotificacao
    {
        Geral,
        Modulo,
        Individual,
    }
}
