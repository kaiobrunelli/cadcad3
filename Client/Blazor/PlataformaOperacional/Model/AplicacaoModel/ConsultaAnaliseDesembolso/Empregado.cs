namespace PlataformaOperacional.Model.AplicacaoModel.ConsultaAnaliseDesembolso;

public class Empregado
{
    public string   Id                { get; set; } = "";
    public string   Matricula         { get; set; } = "";
    public string   Nome              { get; set; } = "";
    public string   Iniciais          { get; set; } = "";
    public string   Cargo             { get; set; } = "";
    public string   Cor               { get; set; } = "#005CA9";
    public string[] Modulos           { get; set; } = [];
    public string   CodigoCoordenacao { get; set; } = "";
}
