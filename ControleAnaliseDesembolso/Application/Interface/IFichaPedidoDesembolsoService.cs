using ControleAnaliseDesembolso.Application.Dtos.Request;
using ControleAnaliseDesembolso.Application.Dtos.Response;

namespace ControleAnaliseDesembolso.Application.Interface
{
    public interface IFichaPedidoDesembolsoService
    {
        Task SalvarFpd(PedidoDesembolsoRequest Fpd, CancellationToken cancellationToken = default);
        Task<PedidoConsultaContratoAfResponse> SolicitarDadosFPD(PedidoConsultaContratoAfRequest Pedido, CancellationToken cancellationToken = default);
    }
}
