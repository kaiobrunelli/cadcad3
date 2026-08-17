using ControleAnaliseDesembolso.Application.Dtos.Response;
using ControleAnaliseDesembolso.Application.Interface;

namespace ControleAnaliseDesembolso.Application
{
    // FAKE — fica só dentro do CAD enquanto não existe integração real com
    // o sistema interno. Simula o que o serviço de verdade devolveria: uma
    // lista fixa de contratos conhecidos (os mesmos usados no seed de teste),
    // com um teto padrão pra qualquer contrato que não estiver na lista
    // (não tem como saber o teto real de um contrato que não existe de
    // verdade nesse ambiente). Trocar por uma implementação real (chamando
    // a macro/RedeCaixaUtilitario) é só registrar outra classe no DI, sem
    // mexer no ValidadorDesembolsoService.
    public class ValoresReferenciaContratoServiceFake : IValoresReferenciaContratoService
    {
        private static readonly Dictionary<string, decimal> _tetosConhecidos = new()
        {
            ["1234567-1"] = 1_000_000m,
            ["7654321-2"] = 500_000m,
            ["1112223-3"] = 50_000m,
        };

        private const decimal TetoPadrao = 1_000_000m;

        public Task<ValoresReferenciaContrato> ObterAsync(string coContratoAf, string coContratoAfDv)
        {
            var chave = $"{coContratoAf}-{coContratoAfDv}";
            var teto = _tetosConhecidos.TryGetValue(chave, out var valor) ? valor : TetoPadrao;

            return Task.FromResult(new ValoresReferenciaContrato
            {
                CoContratoAf = coContratoAf,
                CoContratoAfDv = coContratoAfDv,
                ValorTetoContrato = teto,
            });
        }
    }
}
