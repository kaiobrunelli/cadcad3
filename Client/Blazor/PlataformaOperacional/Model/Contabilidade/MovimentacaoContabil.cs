using PlataformaOperacional.Model.Contabilidade;

namespace PlataformaOperacional.Model.Contabilidade
{
    public class MovimentacaoContabil
    {
        public string? Matricula {  get; set; }
        public string? Senha { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public ICollection<NumeracaoContrato>? NumeracaoContratos { get; set; }
    }
}
