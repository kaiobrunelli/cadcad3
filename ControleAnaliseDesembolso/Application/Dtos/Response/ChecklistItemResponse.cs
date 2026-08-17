using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Application.Dtos.Response
{
    public class ChecklistItemResponse
    {
        public int CoValidacao { get; set; }
        public string? DeValidacao { get; set; }
        public TipoSituacaoValidacao Situacao { get; set; }
        public List<ComentarioValidacaoResponse> Comentarios { get; set; } = new();
    }
}
