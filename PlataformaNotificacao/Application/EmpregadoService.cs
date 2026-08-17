using PlataformaNotificacao.Application.Interface;
using PlataformaNotificacao.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaNotificacao.Application
{
    public class EmpregadoService : IEmpregadoService
    {
        // A matrícula (c123456) é a identidade única — a MESMA que o front envia no
        // ?userId= e usa em ServicoUsuario. Sem esse alinhamento, notificações
        // individuais persistem para uma matrícula que ninguém consulta.
        private static readonly List<Empregado> _todos =
        [
        // Ana Lima e Carla Mendes simulam duas GIGOVs diferentes (cada uma com
        // seu próprio código, GIGOV01/GIGOV02, pra não caírem no filtro de
        // coordenação "06" e se auto-notificarem quando comentam como GIGOV).
        // Os demais (Bruno, Diego, Elena, Kaio) são CEFGA — coordenação "06",
        // o código real usado pelas notificações do CAD (ver
        // ControleAnaliseDesembolsoService.CodigoCoordenacaoCefga).
        new() { Matricula = "c123456", Nome = "Ana Lima",         Iniciais = "AL", Cargo = "Analista Sênior",     Cor = "#005CA9", Modulos = ["Sipub", "Cobranca"],                                  CodigoCoordenacao = "GIGOV01" },
        new() { Matricula = "c102944", Nome = "Bruno Costa",      Iniciais = "BC", Cargo = "Gestor",              Cor = "#065F46", Modulos = ["Sipub", "EncontroDeContas"],                         CodigoCoordenacao = "06" },
        new() { Matricula = "c134872", Nome = "Carla Mendes",     Iniciais = "CM", Cargo = "Analista Júnior",     Cor = "#7C3AED", Modulos = ["Sipub"],                                             CodigoCoordenacao = "GIGOV02" },
        new() { Matricula = "c110233", Nome = "Diego Santos",     Iniciais = "DS", Cargo = "Coordenador",         Cor = "#B45309", Modulos = ["Cobranca", "Amortizacao"],                           CodigoCoordenacao = "06" },
        new() { Matricula = "c145097", Nome = "Elena Ferreira",   Iniciais = "EF", Cargo = "Diretora Financeira", Cor = "#BE185D", Modulos = ["Sipub", "Cobranca", "Amortizacao", "EncontroDeContas"], CodigoCoordenacao = "06" },
        new() { Matricula = "c151896", Nome = "Kaio KBS",   Iniciais = "KB", Cargo = "Programador", Cor = "#BE185D", Modulos = ["Sipub", "Cobranca", "Amortizacao", "EncontroDeContas"], CodigoCoordenacao = "06" },
    ];

        public List<Empregado> ObterTodos() => _todos;

        public Empregado? ObterPorMatricula(string matricula) =>
            _todos.FirstOrDefault(e => e.Matricula == matricula);

        public List<string> ObterMatriculasTodos() =>
            _todos.Select(e => e.Matricula).ToList();

        public List<string> ObterMatriculasPorModulo(string modulo) =>
            _todos.Where(e => e.Modulos.Contains(modulo)).Select(e => e.Matricula).ToList();

        public List<string> ObterMatriculasPorCoordenacao(string codigoCoordenacao) =>
            _todos.Where(e => e.CodigoCoordenacao == codigoCoordenacao).Select(e => e.Matricula).ToList();

        // Recebe matrículas e devolve só as que existem (descarta inválidas)
        public List<string> FiltrarMatriculasValidas(IEnumerable<string> matriculas) =>
            _todos.Where(e => matriculas.Contains(e.Matricula)).Select(e => e.Matricula).ToList();
    }
}
public class Empregado
{
    // Matrícula é a identidade única do empregado (formato c123456). Não há mais "Id".
    public string Matricula { get; set; } = "";
    public string Nome { get; set; } = "";
    public string Iniciais { get; set; } = "";
    public string Cargo { get; set; } = "";
    public string Cor { get; set; } = "#005CA9";
    public string[] Modulos { get; set; } = [];
    public string CodigoCoordenacao { get; set; } = "";
}