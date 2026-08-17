using PlataformaOperacional.Pages.Aplicacao.Desembolsos;
using static PlataformaOperacional.Pages.Aplicacao.Desembolsos.DesembolsosDialogConfirmacao;

namespace PlataformaOperacional.Model.DesembososModel;


public class DesembolsosDialogConfirmacaoResult
{
    public UserAction Action { get; set; }
    public string? Justificativa { get; set; }

    public string? Nmg { get; set; }
    public string? NmgDv { get; set; }

    public string? Drp { get; set; }
    public string? DrpDv { get; set; }
    public string? DrpSenha { get; set; }
}

