using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Domain.Entitys;

public class Desembolso
{
    public int CoDesembolso { get; set; }

    public int CoFpd { get; set; }

    public string? ResponsavelAnalise { get; set; }

    public string? ResponsavelBaixa { get; set; }

    public string? MatriculaBaixa { get; set; }

    public string? Gestor { get; set; }

    public DateTime DtPrazo { get; set; }

    public TipoStatusDesembolso StatusDesembolso { get; set; }

    public DateTime? DtConclusao { get; set; }

    public FichaPedidoDesembolso FichaPedidoDesembolso { get; set; } = null!;

    public ICollection<ValidacaoDesembolso> ValidacoesDesembolso { get; set; }
        = new List<ValidacaoDesembolso>();
}
