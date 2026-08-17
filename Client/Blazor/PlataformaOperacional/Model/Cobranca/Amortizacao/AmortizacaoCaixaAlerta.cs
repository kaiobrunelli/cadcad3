using MudBlazor;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
	public class AmortizacaoCaixaAlerta
	{
		[JsonPropertyName("alerta")]
		public string MensagemDeAlerta { get; set; } = "";
		[JsonPropertyName("severity")]
		public string TipoSeverity { get; set; } = "";

		[NotMapped]
		public Severity AlertaSeverity
		{
			get
			{
				if (Enum.TryParse<Severity>(TipoSeverity, true, out var result))
				{				
					return result;
				}	
				return Severity.Normal;
			}
		}
	}
}
