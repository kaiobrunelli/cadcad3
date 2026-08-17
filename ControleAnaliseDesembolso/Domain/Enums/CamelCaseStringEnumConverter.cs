using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControleAnaliseDesembolso.Domain.Enums
{
    public class CamelCaseStringEnumConverter : JsonStringEnumConverter
    {
        public CamelCaseStringEnumConverter() : base(JsonNamingPolicy.CamelCase) { }
    }
}
