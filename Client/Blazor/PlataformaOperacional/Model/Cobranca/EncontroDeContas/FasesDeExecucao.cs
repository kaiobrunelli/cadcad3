namespace PlataformaOperacional.Model.Cobranca.EncontroDeContas
{
    public enum FasesDeExecucao
    {
        Selecao = 0,
        Validacao = 1,
        Execucao = 2,
        Conclusao = 3
    }

    public static class FasesDeExecucaoExtensions
    {
        public static string Name(this FasesDeExecucao p) => p switch
        {
            FasesDeExecucao.Selecao => "Seleção",
            FasesDeExecucao.Validacao => "Validação",
            FasesDeExecucao.Execucao => "Execução",
            FasesDeExecucao.Conclusao => "Conclusão",
            _ => p.ToString()
        };

        public static string SubLabel(this FasesDeExecucao p) => p switch
        {
            FasesDeExecucao.Selecao => "Contratos",
            FasesDeExecucao.Validacao => "Dados",
            FasesDeExecucao.Execucao => "Macro",
            FasesDeExecucao.Conclusao => "Relatório",
            _ => string.Empty
        };

    }
}
