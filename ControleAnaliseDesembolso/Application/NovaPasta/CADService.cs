using ControleAnaliseDesembolso.Application.Interface;
using ControleAnaliseDesembolso.Infra.Data.Context;
using PlataformaNotificacao.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAnaliseDesembolso.Application.NovaPasta
{
    public class CADService
    {
        private readonly ControleAnaliseDesembolsoContext _context;
        private readonly IEmpregadoCADService _empregados;

        private readonly INotificacaoService _notificacoes;

        public CADService(ControleAnaliseDesembolsoContext context, IEmpregadoCADService empregados, INotificacaoService notificacoes)
        {
            _context = context;
            _empregados = empregados;
            _notificacoes = notificacoes;
        }


    }
}
