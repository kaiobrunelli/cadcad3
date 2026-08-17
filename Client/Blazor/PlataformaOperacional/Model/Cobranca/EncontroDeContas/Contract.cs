namespace PlataformaOperacional.Model.Cobranca.EncontroDeContas
{

    public class Contract
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public string Number { get; set; } = string.Empty;
        public TipoDeMovimentacao Type { get; set; } = TipoDeMovimentacao.CCI;
        public string Holder { get; set; } = string.Empty;
        public ContractStatus Status { get; set; } = ContractStatus.Ready;

        public string HolderFor(TipoDeMovimentacao type) => type switch
        {
            TipoDeMovimentacao.CCI => "Cliente pessoa jurídica",
            TipoDeMovimentacao.DRP => "Carteira DRP",
            TipoDeMovimentacao.DevCaixa => "Devolução Caixa",
            _ => "Cliente pessoa física"
        };
    }

    public enum TipoDeMovimentacao
    {
        CCI,
        AeCaixa,
        DevCaixa,
        DRP
    }

    public enum ContractStatus
    {
        Ready,
        Processing,
        Done,
        Error
    }

    public static class ContractTypeExtensions
    {
        public static string Label(this TipoDeMovimentacao t) => t switch
        {
            TipoDeMovimentacao.AeCaixa => "AE-CAIXA",
            TipoDeMovimentacao.DevCaixa => "DEV-CAIXA",
            _ => t.ToString()
        };
    }

}