using PlataformaNotificacao.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaNotificacao.Domain.DTO
{
    public class NotificacaoDto
    {
        public int CodigoNotificacao { get; set; }
        public CodigoAplicativo? CodigoAplicativo { get; set; }
        public string Titulo { get; set; } = "";
        public string Mensagem { get; set; } = "";
        public TipoNotificacao Tipo { get; set; } = TipoNotificacao.Normal;
        public bool ExigeConfirmacao => Tipo == TipoNotificacao.Urgente;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public DateTime? DataVisualizacao { get; set; }
        public string? Link { get; set; }
    }
}
