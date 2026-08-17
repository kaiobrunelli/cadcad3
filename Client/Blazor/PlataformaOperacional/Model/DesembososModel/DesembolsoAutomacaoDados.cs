namespace PlataformaOperacional.Model.DesembososModel;

public class DesembolsoAutomacaoDados(string senha, int? coControle = null, List<int>? controlesBaixa = null)
{
    public string Senha { get; set; } = senha;
    public int? CoControle { get; set; } = coControle;
    public List<int>? ControlesBaixa { get; set; } = controlesBaixa;
}
