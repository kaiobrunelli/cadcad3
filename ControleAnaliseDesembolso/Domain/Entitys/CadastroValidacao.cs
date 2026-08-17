namespace ControleAnaliseDesembolso.Domain.Entitys;

public class CadastroValidacao
{
    public int CoValidacao { get; set; }

    public string? DeValidacao { get; set; }

    public DateTime DtValidacao { get; set; }

    public string? CampoVinculado { get; set; }

    public string? UsuarioExclusao { get; set; }

    public DateTime? DtExclusao { get; set; }

    public bool Desativado { get; set; }

    public ICollection<ValidacaoDesembolso> ValidacoesDesembolso { get; set; }
        = new List<ValidacaoDesembolso>();
}
