using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Application.Dtos.Request
{
    public class ValidacaoRegistroRequest
    {
        public string? DeRegistro { get; set; }
        public TipoRegistro TipoRegistro { get; set; }
        public string MatriculaAutor { get; set; } = string.Empty;
        public string? NomeAutor { get; set; }
        public int UnidadeAutor { get; set; }
    }
}
