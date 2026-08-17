namespace ControleAnaliseDesembolso.Application.Dtos.Request
{
    public class ValidacaoDesembolsoRequest
    {
        public int CoValidacao { get; set; }

        public int CoDesembolso { get; set; }

        public ValidacaoRegistroRequest ValidacaoRegistro { get; set; } = new();
    }
}
