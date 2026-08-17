using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.DesembososModel;

public class Desembolso
{
    [JsonPropertyName("CoControle")]
    public int? CoControle { get; set; }
    [JsonPropertyName("CoDesembolso")]
    public int? CoDesembolso { get; set; }
    [JsonPropertyName("NuContrato")]
    public string? NuContrato { get; set; }
    [JsonPropertyName("CoControleExecucao")]
    public int? CoControleExecucao { get; set; }
    [JsonPropertyName("NuContratoDv")]
    public string? NuContratoDv { get; set; }
    [JsonPropertyName("TipoMovimentacao")]
    public string? TipoMovimentacao { get; set; }
    [JsonPropertyName("DtCredito")]
    public DateTime? DtCredito { get; set; }
    [JsonPropertyName("IdDrp")]
    public int? IdDrp { get; set; }
    [JsonPropertyName("DrpNumero")]
    public string? DrpNumero { get; set; }
    [JsonPropertyName("DrpDv")]
    public string? DrpDv { get; set; }
    [JsonPropertyName("LogAcaoUsuario")]
    public string? LogAcaoUsuario { get; set; }
    [JsonPropertyName("Sitrf")]
    public int Sitrf { get; set; } = 0;
    [JsonPropertyName("IcOrigem")]
    public string? IcOrigem { get; set; }
    [JsonPropertyName("Tomador")]
    public string? Tomador { get; set; }
    [JsonPropertyName("VrTo01")]
    public decimal? VrTo01 { get; set; }
    [JsonPropertyName("VrTo02")]
    public decimal? VrTo02 { get; set; }
    [JsonPropertyName("VrTo03")]
    public decimal? VrTo03 { get; set; }
    [JsonPropertyName("VrTo04")]
    public decimal? VrTo04 { get; set; }
    [JsonPropertyName("VrTo05")]
    public decimal? VrTo05 { get; set; }
    [JsonPropertyName("VrTo06")]
    public decimal? VrTo06 { get; set; }
    [JsonPropertyName("VrTo07")]
    public decimal? VrTo07 { get; set; }
    [JsonPropertyName("VrTotal")]
    public decimal? VrTotal { get; set; }
    [JsonPropertyName("Fid")]
    public string? Fid { get; set; }
    [JsonPropertyName("Log_Siapf")]
    public string? Log_Siapf { get; set; }
    [JsonPropertyName("HistoricoMovimentacao")]
    public string? HistoricoMovimentacao { get; set; }
    [JsonPropertyName("Situacao")]
    public string? Situacao { get; set; }
    [JsonPropertyName("DescSituacao")]
    public string? DescSituacao { get; set; }
}
