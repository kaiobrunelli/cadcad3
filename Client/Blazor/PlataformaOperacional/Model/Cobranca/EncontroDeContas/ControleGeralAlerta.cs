namespace PlataformaOperacional.Model.Cobranca.EncontroDeContas
{
    public class ControleGeralAlerta
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];

        /// <summary>Info | Warn | Ok | Err</summary>
        public string Tipo { get; set; } = "Info";

        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }
    }
}