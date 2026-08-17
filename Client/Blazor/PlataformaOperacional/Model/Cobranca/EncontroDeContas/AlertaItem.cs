namespace PlataformaOperacional.Model.Cobranca.EncontroDeContas
{


    public class AlertaItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public TipoDeAlerta TipoAlerta { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Desc { get; set; }
    }

    public enum TipoDeAlerta
    {
        Info,
        Warn,
        Ok,
        Err
    }

 

    public enum LogLevel
    {
        Info,
        Ok,
        Warn,
        Err
    }

 
}