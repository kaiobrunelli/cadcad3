using MudBlazor;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaOperacional.Model.Plataforma
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

        [NotMapped]
        public Severity AlertaSeverity
        {
            get
            {
                if (Enum.TryParse<Severity>(Severity, true, out var result))
                {
                    return result;
                }
                return MudBlazor.Severity.Normal;
			}
			set
			{
				Severity = value.ToString();
			}


		}
	}
}
