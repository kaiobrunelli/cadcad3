namespace PlataformaOperacional.Model.DesembososModel;

public class TratarDesembolso(
    string nuContrato,
    string fid,
    string nmg,
    string nmgDv,
    string drp,
    string drpDv,
    string drpSenha)
{
    public string NuContrato { get; } = nuContrato;
    public string Fid { get; } = fid;
    public string Nmg { get; } = nmg;
    public string NmgDv { get; } = nmgDv;
    public string Drp { get; } = drp;
    public string DrpDv { get; } = drpDv;
    public string DrpSenha { get; } = drpSenha;
}