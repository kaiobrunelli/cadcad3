using ControleAnaliseDesembolso.Application.Dtos.Response;

namespace ControleAnaliseDesembolso.Application.Interface
{
    // Abstrai a consulta em tempo real ao sistema interno do contrato.
    // Implementação real (fora do CAD) chamaria a macro/rede de verdade;
    // aqui dentro do CAD usamos ValoresReferenciaContratoServiceFake.
    public interface IValoresReferenciaContratoService
    {
        Task<ValoresReferenciaContrato> ObterAsync(string coContratoAf, string coContratoAfDv);
    }
}
