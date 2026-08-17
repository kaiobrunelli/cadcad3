namespace SipubDesembolsos.Client.Servicos;

public class ServicoUsuario
{
    private string _usuarioId = "usuario-a";

    public string UsuarioId => _usuarioId;

    public string Nome => _usuarioId switch
    {
        "usuario-a" => "Ana Lima",
        "usuario-b" => "Bruno Costa",
        _           => _usuarioId
    };

    public string Iniciais => _usuarioId switch
    {
        "usuario-a" => "AL",
        "usuario-b" => "BC",
        _           => "?"
    };

    public string Cargo => _usuarioId switch
    {
        "usuario-a" => "Analista Sênior",
        "usuario-b" => "Gestor",
        _           => ""
    };

    public string Cor => _usuarioId switch
    {
        "usuario-a" => "#005CA9",
        "usuario-b" => "#065F46",
        _           => "#6B7280"
    };

    public event Func<Task>? AoMudar;

    public async Task MudarParaAsync(string usuarioId)
    {
        _usuarioId = usuarioId;
        if (AoMudar is not null)
            await AoMudar.Invoke();
    }
}
