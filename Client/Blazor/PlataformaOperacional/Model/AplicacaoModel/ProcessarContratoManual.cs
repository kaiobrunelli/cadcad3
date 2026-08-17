using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.AplicacaoModel
{
    public class ProcessarContratoManual
    {
        [JsonPropertyName("coOperacao")] public string CoOperacao { get; set; }
        [JsonPropertyName("desc")] public string Descricao { get; set; }

        public ProcessarContratoManual(string coOperacao, string descricao)
        {
			CoOperacao = coOperacao;
            Descricao = descricao;
        }
    }
    
}
