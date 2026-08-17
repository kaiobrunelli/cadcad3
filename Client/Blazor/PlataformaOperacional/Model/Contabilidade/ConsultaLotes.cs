namespace PlataformaOperacional.Model.Contabilidade
{
    public class ConsultaLotes
    {
        public int NumeroDoLote { get; set; }
        public string Responsavel {  get; set; } = string.Empty;
        public DateTime DataCargaInicio { get; set; }
        public DateTime DataCargaFinal { get; set; }
        public DateTime DataInicialInformada { get; set; }
        public DateTime DataFinalInformada { get; set; }
        public bool Carregado { get; set; }
        public bool Processado { get; set; }
        public int QtdeProcessados { get; set; }
    }
}

