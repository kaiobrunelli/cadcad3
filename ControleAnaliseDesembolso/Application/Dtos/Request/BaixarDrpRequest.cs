namespace ControleAnaliseDesembolso.Application.Dtos.Request
{
    public class BaixarDrpRequest
    {
        public List<int> Ids { get; set; } = [];
        public string MatriculaUsuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
