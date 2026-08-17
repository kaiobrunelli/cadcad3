using ControleAnaliseDesembolso.Application.Dtos.Request;
using ControleAnaliseDesembolso.Application.Dtos.Response;

namespace ControleAnaliseDesembolso.Interface
{
    public interface IControleAnaliseDesembolso
    {
        Task<string?> ObterREsponsavelDesembolso(int coFpd, CancellationToken cancellationToken = default);

        Task CriarFichaPedidoDesembolso(PedidoDesembolsoRequest request, CancellationToken cancellationToken = default);
        Task ReenviarFichaPedidoDesembolso(int coFpd, PedidoDesembolsoRequest request, CancellationToken cancellationToken = default);
        Task AdicionarComentario(ValidacaoDesembolsoRequest request, CancellationToken cancellationToken = default);
        Task EditarComentario(EditarComentarioRequest request, CancellationToken cancellationToken = default);
        Task RemoverComentario(int coRegistroValidacao, string matriculaSolicitante, CancellationToken cancellationToken = default);
        Task ValidarDesembolso(int coDesembolso, CancellationToken cancellationToken = default);
        Task ValidarTodosPendentes(CancellationToken cancellationToken = default);
        Task<List<DesembolsoResponse>> ObterTodosDesembolsos(CancellationToken cancellationToken = default);
        Task VincularResponsavel(int coDesembolso, string? matriculaResponsavel, CancellationToken cancellationToken = default);
        Task<List<ValidacaoTemplateResponse>> ObterValidacoesTemplate(CancellationToken cancellationToken = default);
        Task<List<ComentarioValidacaoResponse>> ObterComentarios(int coDesembolso, CancellationToken cancellationToken = default);
        Task<DesembolsoDetalheResponse> ObterDetalheDesembolso(int coDesembolso, CancellationToken cancellationToken = default);
        Task AprovarDesembolso(int coDesembolso, AprovarDesembolsoRequest request, CancellationToken cancellationToken = default);
        Task BaixarDRP(int coDesembolso, CancellationToken cancellationToken = default);
        Task RejeitarDesembolso(int coDesembolso, RejeitarDesembolsoRequest request, CancellationToken cancellationToken = default);
        Task<List<RegistroDrpResponse>> ObterRegistrosDrp(CancellationToken cancellationToken = default);
        Task BaixarDrpEmLote(BaixarDrpRequest request, CancellationToken cancellationToken = default);
    }
}
