using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Domain.Entitys
{
    public class ValidacaoRegistro
    {
        public int CoRegistroValidacao { get; set; }
        public int CoValidacao { get; set; }
        public int CoDesembolso { get; set; }
        public string? DeRegistro { get; set; }
        public TipoRegistro TipoRegistro { get; set; }
        public string? CoUsuario { get; set; }
        public string? DeUsuario { get; set; }
        public int UnidadeUsuario { get; set; }
        public DateTime DtCriacao { get; set; } = DateTime.Now;

        // Soft-delete: nunca apagamos um comentário de verdade, só marcamos
        // como inativo pra sumir do front. Histórico fica intacto no banco.
        public bool Ativo { get; set; } = true;

        // Derivado, não precisa de coluna própria — a sigla é sempre função do
        // código de unidade já gravado. Unidade 7175 = CEFGA (quem analisa);
        // qualquer outra = GIGOV (quem solicita/preenche a FPD).
        public string SiglaUsuario => UnidadeUsuario == 7175 ? "CEFGA" : "GIGOV";
    }
}
