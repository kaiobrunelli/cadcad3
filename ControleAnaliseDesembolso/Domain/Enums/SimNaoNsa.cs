using System.Text.Json.Serialization;

namespace ControleAnaliseDesembolso.Domain.Enums
{
    [JsonConverter(typeof(CamelCaseStringEnumConverter))]
    public enum SimNaoNsa
    {
        Nao,
        Sim,
        Nsa,
    }
}
