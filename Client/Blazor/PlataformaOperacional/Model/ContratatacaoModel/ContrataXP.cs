using System.Text.Json.Serialization;

namespace PlataformaOperacional.Model.ContratatacaoModel
{
    public class ContrataXP
    {
        //public VerificacoesContratacao(int idChecklist, string deVerificacao,
        //     string contrato, DateTime? dtAnalise, string? resposta, string? observacao, bool temObs = false)
        //{
        //    IdChecklist = idChecklist;
        //    DeVerificacao = deVerificacao;
        //    Contrato = contrato;
        //    DtAnalise = dtAnalise;
        //    Resposta = resposta;
        //    Observacao = observacao;
        //    TemObs = temObs;
        //}

        [JsonPropertyName("idChecklist")]
        public int IdChecklist { get; set; }
        [JsonPropertyName("deVerificacao")]
        public string? DeVerificacao { get; set; } = "";
        [JsonPropertyName("dtAnalise")]
        public DateTime? DtAnalise { get; set; }
        [JsonPropertyName("resposta")]
        public int Resposta { get; set; }
        [JsonPropertyName("temObs")]
        public bool TemObs { get; set; }
        [JsonPropertyName("observacao")]
        public string? Observacao { get; set; }
    }
}
