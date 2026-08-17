using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.CRF
{
    public class ConsultaCrf
    {
        [JsonPropertyName("botaoExecutar")]
        public bool BotaoExecutar { get; set; } = true;
        [JsonPropertyName("consultas")]
        public List<ConsultaCrfLista> ConsultasAtuais { get; set; }
    }
}
