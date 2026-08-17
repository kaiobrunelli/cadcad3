using PlataformaNotificacao.Application.Interface;
using PlataformaNotificacao.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaNotificacao.Application.Interface
{
    public interface IEmpregadoService
    {
        List<string> ObterMatriculasTodos();
        List<string> ObterMatriculasPorModulo(string modulo);
        List<string> ObterMatriculasPorCoordenacao(string codigoCoordenacao);
        List<string> FiltrarMatriculasValidas(IEnumerable<string> matriculas);
    }
}



