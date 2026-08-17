using ControleAnaliseDesembolso.Application.Interface;
using ControleAnaliseDesembolso.Domain.Entitys;

namespace ControleAnaliseDesembolso.Application
{
    // MOCK — simula a futura tabela de empregados/responsáveis do CAD.
    // Quando ela existir, troca a lista fixa por uma consulta ao _context,
    // sem mudar a interface (quem consome não muda).
    public class EmpregadoCADService : IEmpregadoCADService
    {
        private static readonly List<Empregado> _todos =
        [
            new() { Matricula = "c123456", Nome = "Karina Souto",   Iniciais = "KS", Cor = "#005CA9" },
            new() { Matricula = "c134872", Nome = "Rafael Mendes",  Iniciais = "RM", Cor = "#6D28D9" },
            new() { Matricula = "c110233", Nome = "Ana Paula Lima", Iniciais = "AP", Cor = "#0E7490" },
            new() { Matricula = "c145097", Nome = "Bruno Costa",    Iniciais = "BC", Cor = "#065F46" },
            new() { Matricula = "c151896", Nome = "Tânia Ferreira", Iniciais = "TF", Cor = "#92400E" },
        ];

        public Task<List<Empregado>> ObterTodos() => Task.FromResult(_todos);

        public Task<Empregado?> ObterPorMatricula(string matricula) =>
            Task.FromResult(_todos.FirstOrDefault(e => e.Matricula == matricula));
    }
}
