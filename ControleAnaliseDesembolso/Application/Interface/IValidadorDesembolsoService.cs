using ControleAnaliseDesembolso.Application.Dtos.Response;
using ControleAnaliseDesembolso.Domain.Entitys;

namespace ControleAnaliseDesembolso.Application.Interface
{
    public interface IValidadorDesembolsoService
    {
        // Confronta os dados da FPD com os valores de referência de outro
        // serviço (ex: teto de valor do contrato) e devolve, para cada
        // CoValidacao que sabe checar, se passou ou não e a mensagem
        // explicando o motivo. Quem não é retornado aqui simplesmente não
        // é uma validação automática (fica só no manual).
        Task<List<ResultadoValidacaoAutomatica>> Validar(FichaPedidoDesembolso fpd);
    }
}
