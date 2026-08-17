namespace PlataformaOperacional.Model.DesembososModel
{
    public class DesembolsoFluxoDto(string contrato, string fid)
    {
        public string Contrato { get; } = contrato;
        public string FID { get; } = fid;
    }
}
