using PlataformaNotificacao.Domain.Enum;
using System.Text.Json.Serialization;

namespace PlataformaNotificacao.Domain
{
    public class MensagemNotificacao
    {
        public int CodigoNotificacao { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
        public TipoNotificacao Tipo { get; set; } = TipoNotificacao.Normal;
        public EscopoNotificacao Escopo { get; set; } = EscopoNotificacao.Geral;
        public CodigoAplicativo? CodigoAplicativo { get; set; }
        public bool ExigeConfirmacao => Tipo == TipoNotificacao.Urgente;
        public string? Link { get; set; }
        public DateTime? DataValidade { get; set; }

        // Nome do método SignalR invocado (SendAsync). Fixo hoje, mas exposto
        // como campo para permitir eventos diferentes no futuro sem quebrar
        // os chamadores existentes (todos caem no default).
        [JsonIgnore]
        public string ChaveConexao { get; set; } = "ReceberNotificacao";

        // Roteamento server-side (Clients.Groups(...)) — nunca deve ser
        // serializado/enviado ao client, por isso [JsonIgnore]. Fica aqui só
        // para o publicador (NotificacaoService) não precisar de um wrapper
        // extra ao redor do conteúdo.
        [JsonIgnore]
        public List<string> Destinatarios { get; set; } = [];
    }
}
