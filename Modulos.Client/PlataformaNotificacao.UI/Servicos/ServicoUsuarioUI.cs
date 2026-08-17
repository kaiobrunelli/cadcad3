namespace PlataformaNotificacao.UI.Servicos;

public class ServicoUsuarioUI
{
    // Editável rápido: pra testar como GIGOV ou CEFGA com a SUA própria
    // identidade (sem trocar pra outra matrícula da lista, que muda
    // nome/cargo/cor junto), mude só o Unidade aqui. 7175 = CEFGA,
    // qualquer outro valor = GIGOV.
    private class UsuarioMock
    {
        public string Nome { get; set; } = "";
        public string Matricula { get; set; } = "";
        public int Unidade { get; set; }
    }

    private readonly UsuarioMock UsuarioTeste = new()
    {
        Nome = "Kaio Brunelli",
        Matricula = "c151896",
        Unidade = 7175,
    };

    // Identidade = matrícula (c123456), a MESMA que vai no ?matriculaUsuario= do
    // SignalR e do REST, e que o EmpregadoService/banco usam. Não existe "id" separado.
    private string _matricula;

    public ServicoUsuarioUI()
    {
        _matricula = UsuarioTeste.Matricula;
    }

    public string Matricula => _matricula;

    public string Nome => _matricula == UsuarioTeste.Matricula
        ? UsuarioTeste.Nome
        : _matricula switch
        {
            "c123456" => "Bruno Costa",
            "c123457" => "Carla Mendes",
            "c123000" => "Diego Santos",
            "c123001" => "Elena Ferreira",
            _         => _matricula
        };

    public string Iniciais => _matricula switch
    {
        "c151896" => "KB",
        "c123456" => "BC",
        "c123457" => "CM",
        "c123000" => "DS",
        "c123001" => "EF",
        _         => "?"
    };

    public string Cargo => _matricula switch
    {
        "c151896" => "Analista Sênior",
        "c123456" => "Gestor",
        "c123457" => "Analista Júnior",
        "c123000" => "Coordenador",
        "c123001" => "Diretora Financeira",
        _         => ""
    };

    // Unidade do usuário — mesma convenção já usada no servidor pra derivar a
    // sigla dos comentários (ComentarioValidacaoResponse.Sigla): 7175 = CEFGA
    // (quem analisa/aprova), qualquer outra = GIGOV (quem solicita/preenche a
    // FPD e pode editá-la). Kaio e Carla simulam GIGOV; os demais, CEFGA.
    public int UnidadeUsuario => _matricula == UsuarioTeste.Matricula
        ? UsuarioTeste.Unidade
        : _matricula switch
        {
            "c123457" => 7105, // Carla Mendes — GIGOV
            _         => 7175, // Bruno Costa, Diego Santos, Elena Ferreira — CEFGA
        };

    public bool EhGigov => UnidadeUsuario != 7175;

    public string Cor => _matricula switch
    {
        "c151896" => "#005CA9",
        "c123456" => "#065F46",
        "c123457" => "#7C3AED",
        "c123000" => "#B45309",
        "c123001" => "#BE185D",
        _         => "#6B7280"
    };

    public event Func<Task>? AoMudar;

    public async Task MudarParaAsync(string matricula)
    {
        _matricula = matricula;
        if (AoMudar is not null)
            await AoMudar.Invoke();
    }
}
