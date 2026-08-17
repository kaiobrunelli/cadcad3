using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Domain.Entitys;

public class ValidacaoDesembolso
{
    public int CoValidacao { get; set; }

    public int CoDesembolso { get; set; }

    public string? DeValidacao { get; set; }

    public string? CampoVinculado { get; set; }

    public TipoSituacaoValidacao Situacao { get; set; }

    public CadastroValidacao CadastroValidacao { get; set; } = null!;

    public Desembolso Desembolso { get; set; } = null!;
}
