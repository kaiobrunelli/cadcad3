using Plataforma.UI.Shared.Model;
using PlataformaOperacional.Model.AplicacaoModel.ConsultaAnaliseDesembolso;
using PlataformaOperacional.Model.Plataforma;

namespace PlataformaOperacional.Service.Middleware
{
    /// <summary>
    /// Camada de DOMÍNIO das notificações — versão para o SignalRService MÍNIMO
    /// (aquele em que a progress bar ficou 100% intocada).
    ///
    /// Como o transporte mínimo não tem retry próprio de start nem reconstrução,
    /// este serviço assume essas responsabilidades para a notificação:
    ///   • laço suave de conexão (a cada 10s) enquanto estiver offline;
    ///   • rearme quando o retry automático do SignalR desiste (AoDesconectar).
    /// Nada disso toca o fluxo da progress bar.
    ///
    /// Registrar como Singleton e iniciar uma única vez no bootstrap, assim que a
    /// matrícula for resolvida (o componente SinoNotificacoes já faz isso).
    /// </summary>
    public class ServicoNotificacao
    {
        private const string NomeEventoHub = "ReceberNotificacao";

        private readonly SignalRService _signalR;
        private bool _escutaRegistrada;

        /// <summary>Disparado a cada notificação recebida em tempo real (já filtrada).</summary>
        public event Action<MensagemNotificacao>? AoReceberNotificacao;

        /// <summary>Estado da conexão ("Conectado em tempo real" / "Offline").</summary>
        public bool Conectado => _signalR.Conectado;

        public string? UsuarioAtual => _signalR.UsuarioAtual;

        public ServicoNotificacao(SignalRService signalR)
        {
            _signalR = signalR;
        }

        /// <summary>
        /// Conecta com a identidade informada — ou reconecta, se o usuário mudou
        /// (seletor de teste da barra). Idempotente para o mesmo usuário.
        /// </summary>
        public async Task IniciarOuReconectarAsync(string usuarioId)
        {
            // 1º define a identidade: a URL do hub é montada com ela.
            // (Se o usuário mudou, o transporte reconstrói a conexão sozinho.)
            await _signalR.DefinirUsuarioAsync(usuarioId);

            // Escuta registrada uma única vez — sobrevive às reconstruções via fábricas
            if (!_escutaRegistrada)
            {
                _escutaRegistrada = true;
                await _signalR.EscutarEvento<MensagemNotificacao>(NomeEventoHub, TratarMensagemAsync);
            }

            await _signalR.IniciarHubConnection();
        }

        private Task TratarMensagemAsync(MensagemNotificacao msg)
        {
            Console.WriteLine($"[SignalR] Notificação recebida: {msg.Titulo} (escopo: {msg.Escopo})");

            // Notificação que chegou já vencida não deve aparecer para o usuário
            if (msg.DataValidade != default && msg.DataValidade.ToLocalTime() < DateTime.Now)
                return Task.CompletedTask;

            AoReceberNotificacao?.Invoke(msg);
            return Task.CompletedTask;
        }


        ///////////////////////////////////
        ////// <summary>
        /// Alias para compatibilidade com componentes que utilizam
        /// IniciarAsync().
        /// </summary>
        public Task IniciarAsync(string usuarioId)
        {
            return IniciarOuReconectarAsync(usuarioId);
        }
    }

}