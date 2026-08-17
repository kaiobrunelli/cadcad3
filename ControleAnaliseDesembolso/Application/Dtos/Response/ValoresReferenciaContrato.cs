namespace ControleAnaliseDesembolso.Application.Dtos.Response
{
    // Retorno de uma consulta ao sistema interno (a "macro"/rede que o
    // RedeCaixaUtilitario já integra em outro lugar do Julho26) — hoje só
    // tem o campo que as regras atuais realmente usam. As outras ~19
    // informações que existem na base de verdade entram aqui conforme forem
    // sendo consumidas por novas regras; não faz sentido antecipar campo que
    // nenhuma regra usa ainda.
    public class ValoresReferenciaContrato
    {
        public string CoContratoAf { get; set; } = string.Empty;
        public string CoContratoAfDv { get; set; } = string.Empty;
        public decimal ValorTetoContrato { get; set; }
    }
}
