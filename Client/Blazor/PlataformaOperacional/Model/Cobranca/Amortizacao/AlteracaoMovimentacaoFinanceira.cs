using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.Cobranca.Amortizacao
{
    public class AlteracaoMovimentacaoFinanceira
    {
        [JsonPropertyName("coMovimentacaoFinanceira")]
        public int? CoMovimentacaoFinanceira { get; set; } = 0;
        [JsonPropertyName("acaoUsuario")]
        public string AcaoUsuario { get; set; }
        [JsonPropertyName("tiposDeOperacao")]
        public ConjuntoTo? TiposDeOperacao { get; set; }


        public AlteracaoMovimentacaoFinanceira(int? coMovimentacaoFinanceira, string acao, ConjuntoTo to)
        {
			CoMovimentacaoFinanceira = coMovimentacaoFinanceira;
			AcaoUsuario = acao;
			TiposDeOperacao = to;
        }
    }

}
