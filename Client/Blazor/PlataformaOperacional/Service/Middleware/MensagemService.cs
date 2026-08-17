namespace PlataformaOperacional.Service.Middleware
{
    public class MensagemService<T>
    {
        public MensagemService(T? tipoData, string mensagem, bool sucess)
        {
            TipoData = tipoData;
            Mensagem = mensagem;
        }

        public T? TipoData { get; set; }
        public string Mensagem { get; set; }
        public bool Sucess { get; set; }

    }
}
