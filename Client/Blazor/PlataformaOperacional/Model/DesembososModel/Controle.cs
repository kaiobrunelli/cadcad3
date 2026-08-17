using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.DesembososModel;

public class Controle
{
    [JsonPropertyName("CoControle")]
    public int CoControle { get; set; }

    [JsonPropertyName("DtMovimento")]
    public DateTime DtMovimento { get; set; }

    [JsonPropertyName("DtReferencia")]
    public DateTime DtReferencia { get; set; }

    [JsonPropertyName("DtCargaIniciada")]
    public DateTime? DtCargaIniciada { get; set; }

    [JsonPropertyName("QtdTotalApontamentos")]
    public int? QtdTotalApontamentos { get; set; }

    [JsonPropertyName("VlTotalApontamentos")]
    public decimal? VlTotalApontamentos { get; set; }

    [JsonPropertyName("QtdLibcef")]
    public int? QtdLibcef { get; set; }

    [JsonPropertyName("VlLibcef")]
    public decimal? VlLibcef { get; set; }

    [JsonPropertyName("QtdLifgtdes")]
    public int? QtdLifgtdes { get; set; }

    [JsonPropertyName("VlLifgtdes")]
    public decimal? VlLifgtdes { get; set; }

    [JsonPropertyName("QtdLifgtdia")]
    public int? QtdLifgtdia { get; set; }

    [JsonPropertyName("VlLifgtdia")]
    public decimal? VlLifgtdia { get; set; }

    [JsonPropertyName("QtdOgucef")]
    public int? QtdOgucef { get; set; }

    [JsonPropertyName("VlOgucef")]
    public decimal? VlOgucef { get; set; }

    [JsonPropertyName("QtdSubogu")]
    public int? QtdSubogu { get; set; }

    [JsonPropertyName("VlSubogu")]
    public decimal? VlSubogu { get; set; }

    [JsonPropertyName("QtdParceria")]
    public int? QtdParceria { get; set; }

    [JsonPropertyName("VlParceria")]
    public decimal? VlParceria { get; set; }

    [JsonPropertyName("QtdDrp")]
    public int? QtdDrp { get; set; }
    [JsonPropertyName("VlDrp")]
    public decimal? VlDrp { get; set; }

    [JsonPropertyName("ResponsavelEmitirDrp")]
    public string? ResponsavelEmitirDrp { get; set; }

    [JsonPropertyName("DtFimEmitirDrp")]
    public DateTime? DtFimEmitirDrp { get; set; }

    [JsonPropertyName("ResponsavelBaixarDrp")]
    public string? ResponsavelBaixarDrp { get; set; }

    [JsonPropertyName("DtFimBaixarDrp")]
    public DateTime? DtFimBaixarDrp { get; set; }

    [JsonPropertyName("situacaoCarga")]
    public string? SituacaoCarga { get; set; }

    [JsonPropertyName("descSituacaoCarga")]
    public string? DescSituacaoCarga { get; set; }

    [JsonPropertyName("situacaoDesembolsos")]
    public string? SituacaoDesembolsos { get; set; }

    [JsonPropertyName("descSituacaoDesembolsos")]
    public string? DescSituacaoDesembolsos { get; set; }

    [JsonPropertyName("situacaoDrpEmissao")]
    public string? SituacaoDrpEmissao { get; set; }

    [JsonPropertyName("descSituacaoDrpEmissao")]
    public string? DescSituacaoDrpEmissao { get; set; }

    [JsonPropertyName("situacaoDrpBaixa")]
    public string? SituacaoDrpBaixa { get; set; }

    [JsonPropertyName("descSituacaoDrpBaixa")]
    public string? DescSituacaoDrpBaixa { get; set; }

    [JsonPropertyName("situacaoFinalizacao")]
    public string? SituacaoFinalizacao { get; set; }

    [JsonPropertyName("descSituacaoFinalizacao")]
    public string? DescSituacaoFinalizacao { get; set; }
}
