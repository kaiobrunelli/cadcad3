using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Application.Dtos.Request
{
    public class EditarComentarioRequest
    {
        public int CoRegistroValidacao { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string MatriculaSolicitante { get; set; } = string.Empty;

        public TipoRegistro TipoRegistro { get; set; }
    }
}
