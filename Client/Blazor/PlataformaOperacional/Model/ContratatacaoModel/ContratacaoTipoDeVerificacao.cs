using PlataformaOperacional.Model.ContratatacaoModel;
using System.Text.Json.Serialization;


namespace PlataformaOperacional.Model.ContratacaoModel
{
    public class ContratacaoTipoDeVerificacao 
    {            

        //É o bloco/etapa do checklist
        [JsonPropertyName("deTipoVerificacao")]
        public string DeTipoVerificacao { get; set; } = "";
        [JsonPropertyName("total")]
        public int Total { get; set; } = 0;
        [JsonPropertyName("concluidos")]
        public int Concluidos { get; set; } = 0;
        /// <summary>
        ///  Verificação representa o item da conformidade a ser checado.
        /// </summary>
        [JsonPropertyName("verificacoes")]
        public List<ContratacaoVerificacao> ListaVerificacoes { get; set; } = new List<ContratacaoVerificacao>();
      
    }
}
