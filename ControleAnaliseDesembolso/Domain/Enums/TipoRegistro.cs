using System.Text.Json.Serialization;

namespace ControleAnaliseDesembolso.Domain.Enums
{
    [JsonConverter(typeof(CamelCaseStringEnumConverter))]
    public enum TipoRegistro
    {
        Justificativa,
        Informativo,
        Parecer,
        Observacao,
        Rejeicao
    }
}
