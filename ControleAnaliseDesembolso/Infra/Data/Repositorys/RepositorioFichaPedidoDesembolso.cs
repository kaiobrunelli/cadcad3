using ControleAnaliseDesembolso.Domain.Entitys;
using ControleAnaliseDesembolso.Domain.Repositorys;
using ControleAnaliseDesembolso.Infra.Data.Context;

namespace ControleAnaliseDesembolso.Infra.Data.Repositorys
{
    public class RepositorioFichaPedidoDesembolso : RepositorioBase<FichaPedidoDesembolso>, IRepositorioFichaPedidoDesembolso
    {
        public RepositorioFichaPedidoDesembolso(ControleAnaliseDesembolsoContext context) : base(context)
        {
        }
    }
}
