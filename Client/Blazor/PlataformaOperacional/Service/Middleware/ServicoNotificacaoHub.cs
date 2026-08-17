//using Microsoft.AspNetCore.SignalR.Client;
//using PlataformaOperacional.Model.Plataforma;
//using SipubDesembolsos.Shared;

//namespace SipubDesembolsos.Client.Servicos;

//public class ServicoNotificacaoHub : IAsyncDisposable
//{
//    private HubConnection? _conexao;
//    private readonly string _urlHubBase;

//    public event Action<MensagemNotificacao>?  AoReceberNotificacao;
//    public event Action<ObservadorAutomacao>? AoReceberProgresso;
//    public bool Conectado => _conexao?.State == HubConnectionState.Connected;
//    public string? UsuarioAtual { get; private set; }

//    public ServicoNotificacaoHub(string urlBase)
//    {
//        _urlHubBase = $"{urlBase.TrimEnd('/')}/hubs/notificacao";
//    }

//    public async Task IniciarOuReconectarAsync(string usuarioId)
//    {
//        if (UsuarioAtual == usuarioId && _conexao?.State == HubConnectionState.Connected)
//            return;

//        if (_conexao is not null)
//        {
//            await _conexao.StopAsync();
//            await _conexao.DisposeAsync();
//            _conexao = null;
//        }

//        UsuarioAtual = usuarioId;

//        _conexao = new HubConnectionBuilder()
//            .WithUrl($"{_urlHubBase}?userId={usuarioId}")
//            .WithAutomaticReconnect([TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)])
//            .Build();

//        _conexao.On<MensagemNotificacao>("ReceberNotificacao", mensagem =>
//        {
//            Console.WriteLine($"[SignalR] Notificação recebida: {mensagem.Titulo} (escopo: {mensagem.Escopo})");
//            AoReceberNotificacao?.Invoke(mensagem);
//        });

//        _conexao.On<int, ObservadorAutomacao>("ProgressoProcessamento", (_, observer) =>
//        {
//            AoReceberProgresso?.Invoke(observer);
//        });

//        _conexao.Reconnected += id =>
//        {
//            Console.WriteLine($"[SignalR] Reconectado. ConnectionId: {id}");
//            AoReceberNotificacao?.Invoke(new MensagemNotificacao
//            {
//                Titulo           = "Conexão restabelecida",
//                Mensagem         = "A conexão com o servidor foi restabelecida.",
//                Tipo             = TipoNotificacao.Normal,
//                Escopo           = EscopoNotificacao.Geral
//            });
//            return Task.CompletedTask;
//        };

//        _conexao.Closed += erro =>
//        {
//            Console.WriteLine($"[SignalR] Conexão fechada. Erro: {erro?.Message}");
//            return Task.CompletedTask;
//        };

//        try
//        {
//            await _conexao.StartAsync();
//            Console.WriteLine($"[SignalR] Conectado como '{usuarioId}'. ConnectionId: {_conexao.ConnectionId}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"[SignalR] Falha ao conectar: {ex.Message}");
//            _ = Task.Delay(5000).ContinueWith(async _ => await TentarReconectarAsync());
//        }
//    }

//    private async Task TentarReconectarAsync()
//    {
//        if (_conexao is null || _conexao.State == HubConnectionState.Connected) return;
//        try
//        {
//            await _conexao.StartAsync();
//            Console.WriteLine("[SignalR] Reconectado na tentativa manual.");
//        }
//        catch
//        {
//            _ = Task.Delay(5000).ContinueWith(async _ => await TentarReconectarAsync());
//        }
//    }

//    public async ValueTask DisposeAsync()
//    {
//        if (_conexao is not null)
//            await _conexao.DisposeAsync();
//    }
//}
