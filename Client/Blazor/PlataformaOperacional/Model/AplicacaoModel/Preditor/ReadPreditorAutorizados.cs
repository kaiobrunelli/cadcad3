using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Aplicacao.Preditor
{
	public class ReadPreditorAutorizados
	{
		public int CoAutorizado { get; set; }
		public string? NomeOperador { get; set; }
		public string? CoMatricula { get; set; }
		public string? Funcao { get; set; }
		public bool Eventual { get; set; } = false;
		public string? Setor { get; set; }
		public DateTime? DtCadastro { get; set; }
		public string? RespCadastro { get; set; }
		public DateTime? ValidadeAutorizacao { get; set; }
		public DateTime? DtFinalizacao { get; set; }

	}
}
