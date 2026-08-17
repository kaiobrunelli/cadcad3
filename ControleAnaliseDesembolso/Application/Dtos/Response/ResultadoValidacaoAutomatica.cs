namespace ControleAnaliseDesembolso.Application.Dtos.Response
{
    public class ResultadoValidacaoAutomatica
    {
        public int CoValidacao { get; set; }
        public bool Aprovado { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
