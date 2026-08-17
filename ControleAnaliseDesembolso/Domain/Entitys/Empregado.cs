namespace ControleAnaliseDesembolso.Domain.Entitys
{
    // Sem tabela/EF Config própria ainda — hoje vem de uma lista mockada em
    // EmpregadoCADService. Quando a tabela real existir, essa classe já serve
    // de entidade, só falta o DbSet + Config.
    public class Empregado
    {
        public string Matricula { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Iniciais { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
    }
}
