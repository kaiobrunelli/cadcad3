using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaNotificacao.Domain
{
    public class ObservadorAutomacao
    {
        public string NomeProcesso { get; set; } = "";
        public int TotalAProcessar { get; set; }
        public int TotalProcessado { get; set; }
        public int PercentualProcessado { get; set; }
        public string Mensagem { get; set; } = "";
        public string Severity { get; set; } = "";
        public string ChaveConexao { get; set; } = "";
        public int NumeroFaseAtual { get; set; }
        public bool ExecutandoSP { get; set; } = false;
    }
}
