using PlataformaOperacional.Model.Cobranca.Amortizacao;
using PlataformaOperacional.Model.Cobranca.EncontroDeContas;
using PlataformaOperacional.Model.Shared;
using System.Text.Json.Serialization;

public class ReadEncontroDeContasControleDto
{
    [JsonPropertyName("coControle")]
    public int? CoControle { get; set; }

    [JsonPropertyName("amEncontroContas")]
    public string? AmEncontroContas { get; set; }

    [JsonPropertyName("nuQuinzena")]
    public int? NuQuinzena { get; set; }

    [JsonPropertyName("dtProcIni")]
    public DateTime? DtProcIni { get; set; }

    [JsonPropertyName("dtProcFim")]
    public DateTime? DtProcFim { get; set; }

    [JsonPropertyName("dtLimiteEc")]
    public DateTime? DtLimiteEc { get; set; }

    [JsonPropertyName("dtUtilLimiteEc")]
    public DateTime? DtUtilLimiteEc { get; set; }

    [JsonPropertyName("qtdReaberturas")]
    public int? QtdReaberturas { get; set; }

    [JsonPropertyName("qtdCondicionais")]
    public int? QtdCondicionais { get; set; }

    [JsonPropertyName("qtdDrps")]
    public int? QtdDrps { get; set; }

    [JsonPropertyName("usuarioIniciador")]
    public string? UsuarioIniciador { get; set; }

    [JsonPropertyName("dtInicioExecucao")]
    public DateTime? DtInicioExecucao { get; set; }

    [JsonPropertyName("dtFimExecucao")]
    public DateTime? DtFimExecucao { get; set; }

    [JsonPropertyName("deObservacao")]
    public string? DeObservacao { get; set; }

    [JsonPropertyName("situacao")]
    public string? Situacao { get; set; }

    [JsonPropertyName("painelControle")]
    public PainelControle? PainelControle { get; set; }
}

public class PainelControle
{
    [JsonPropertyName("botoes")]
    public BotoesConfig Botoes { get; set; } = new();

    [JsonPropertyName("alertas")]
    public List<PlataformaOperacionalAlerta> Alertas { get; set; } = new();

    [JsonPropertyName("locationSignalR")]
    public string? LocationSignalR { get; set; }
}
public class BotoesConfig
{
    [JsonPropertyName("descExecutar")]
    public string DescExecutar { get; set; } = "";

    [JsonPropertyName("executar")]
    public bool Executar { get; set; }

    [JsonPropertyName("descCancelar")]
    public string DescCancelar { get; set; } = "";

    [JsonPropertyName("cancelar")]
    public bool Cancelar { get; set; }

    [JsonPropertyName("descConfigurar")]
    public string DescConfigurar { get; set; } = "";

    [JsonPropertyName("configurar")]
    public bool Configurar { get; set; }
}