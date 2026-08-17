using ControleAnaliseDesembolso.Componentes;
using ControleAnaliseDesembolso.Modelos;
using System.Net.Http.Json;

namespace ControleAnaliseDesembolso.Service;

public class ControleAnaliseDesembolsoService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Api");

    public async Task<List<DesembolsoCAD>> ObterTodosAsync()
    {
        var lista = await _httpClient.GetFromJsonAsync<List<DesembolsoResponseDto>>("api/ObterTodosDesembolsos");
        return (lista ?? []).Select(MapearParaDesembolsoCAD).ToList();
    }

    public async Task<DesembolsoCAD?> ObterPorIdAsync(string id)
    {
        var todos = await ObterTodosAsync();
        return todos.FirstOrDefault(d => d.Id == id);
    }

    public async Task<DesembolsoDetalheDto?> ObterDetalheDesembolsoAsync(string id) =>
        await _httpClient.GetFromJsonAsync<DesembolsoDetalheDto>($"api/ObterDetalheDesembolso/{id}");

    public static string MapearStatus(int statusServidor, bool dataAgendamento = false)
    {
        if (dataAgendamento && statusServidor is 0 or 1) return "agendado";

        return statusServidor switch
        {
            0 => "pendencia",
            1 => "pendente",
            2 => "aprovado",
            3 => "baixado",
            4 => "negado",
            _ => "pendencia",
        };
    }

    private static DesembolsoCAD MapearParaDesembolsoCAD(DesembolsoResponseDto d) => new()
    {
        Id = d.CoDesembolso.ToString(),
        NumId = d.NumId,
        Contrato = d.Contrato,
        Mutuario = d.Mutuario,
        Gigov = d.Gigov,
        Valor = d.Valor,
        Fase = "",
        ValidacoesOk = d.ValidacoesOk,
        ValidacoesTotal = d.ValidacoesTotal,
        Status = MapearStatus(d.Status, d.DataAgendamento),
        PrazoFinal = d.PrazoFinal,
        DtSolicitado = d.DtSolicitado,
        DtConclusao = d.DtConclusao,
        PrimeiroDesembolso = d.PrimeiroDesembolso,
        UltimoDesembolso = d.UltimoDesembolso,
        Adiantamento = d.Adiantamento,
    };

    public async Task<bool> AprovarAsync(string id, string matriculaUsuario, string usuarioNome)
    {
        var req = new { MatriculaUsuario = matriculaUsuario, UsuarioNome = usuarioNome };
        var resposta = await _httpClient.PutAsJsonAsync($"api/Aprovar/{id}", req);
        return resposta.IsSuccessStatusCode;
    }

    public async Task<bool> RejeitarAsync(string id, string matriculaUsuario, string usuarioNome, string codigoCoordenacao, string justificativa = "")
    {
        var req = new { MatriculaUsuario = matriculaUsuario, UsuarioNome = usuarioNome, CodigoCoordenacao = codigoCoordenacao, Justificativa = justificativa };
        var resposta = await _httpClient.PutAsJsonAsync($"api/Rejeitar/{id}", req);
        return resposta.IsSuccessStatusCode;
    }

    // public async Task<bool> VincularAnalistaAsync(string id, string matriculaAnalista)
    // {
    //     var req = new { MatriculaAnalista = matriculaAnalista };
    //     var resposta = await _httpClient.PutAsJsonAsync($"api/VincularResponsavel/{id}", req);
    //     return resposta.IsSuccessStatusCode;
    // }

    // public async Task<bool> RemoverVinculoAsync(string id)
    // {
    //     var resposta = await _httpClient.DeleteAsync($"api/RemoverResponsavel/{id}");
    //     return resposta.IsSuccessStatusCode;
    // }

    // POST api/Validar/{id} só roda a validação automática e retorna 200 vazio
    // — não devolve corpo nenhum pra ler.
    public async Task<bool> ValidarAsync(string id)
    {
        var resposta = await _httpClient.PostAsync($"api/Validar/{id}", null);
        return resposta.IsSuccessStatusCode;
    }

    // Macro geral da home: roda a validação em lote pra todo desembolso ainda
    // no status inicial (pendência — acabou de ser inserido).
    public async Task<bool> ValidarTodosPendentesAsync()
    {
        var resposta = await _httpClient.PostAsync("api/ValidarTodosPendentes", null);
        return resposta.IsSuccessStatusCode;
    }

    public async Task<List<ValidacaoTemplateDto>> ObterValidacoesTemplateAsync()
    {
        var lista = await _httpClient.GetFromJsonAsync<List<ValidacaoTemplateDto>>("api/ObterValidacoesTemplate");
        return lista ?? [];
    }

    public async Task<bool> AdicionarComentarioAsync(string coDesembolso, int coValidacao, string texto, string tipoRegistro, string matriculaAutor, string nomeAutor, int unidadeAutor)
    {
        var req = new { DeRegistro = texto, TipoRegistro = tipoRegistro, MatriculaAutor = matriculaAutor, NomeAutor = nomeAutor, UnidadeAutor = unidadeAutor };
        //var resposta = await _httpClient.PostAsJsonAsync($"api/AdicionarComentario/{coDesembolso}/validacoes/{coValidacao}/comentarios", req);
        var resposta = await _httpClient.PostAsJsonAsync($"api/AdicionarComentario?coDesembolso={coDesembolso}&coValidacao={coValidacao}", req);
        return resposta.IsSuccessStatusCode;
    }


    public async Task<bool> EditarComentarioAsync(string coDesembolso, int coValidacao, int comentarioId, string novoTexto, string matriculaSolicitante)
    {
        var req = new { Texto = novoTexto, MatriculaSolicitante = matriculaSolicitante };
        var resposta = await _httpClient.PutAsJsonAsync($"api/EditarComentario/{comentarioId}", req);
        return resposta.IsSuccessStatusCode;
    }

    public async Task<bool> RemoverComentarioAsync(string coDesembolso, int coValidacao, int coComentario, string matriculaSolicitante)
    {
        var req = new { CoValidacao = coValidacao, CoDesembolso = coDesembolso, MatriculaSolicitante = matriculaSolicitante };
        var resposta = await _httpClient.PutAsJsonAsync($"api/RemoverComentario/{coComentario}?matriculaSolicitante={Uri.EscapeDataString(matriculaSolicitante)}", req);
        return resposta.IsSuccessStatusCode;
    }

    public async Task<HttpResponseMessage> CriarFichaPedidoDesembolsoAsync(object request) =>
        await _httpClient.PostAsJsonAsync("api/CriarFichaPedidoDesembolso", request);

    public async Task<HttpResponseMessage> ReenviarFichaAsync(int coFpd, object request) =>
        await _httpClient.PutAsJsonAsync($"api/ReenviarFicha/{coFpd}", request);

    // Automação/macro — endpoint de um backend diferente (fora do módulo
    // ControleAnaliseDesembolso), mas centralizado aqui pra nenhum componente
    // precisar injetar HttpClient direto.
    public async Task<bool> ExecutarProcessamentoAsync()
    {
        var resposta = await _httpClient.PostAsync("api/processamento/executar", null);
        return resposta.IsSuccessStatusCode;
    }

    public async Task<HttpResponseMessage> BuscarContratoAFAsync(string coContratoAF, string coContratoAFDV) =>
        await _httpClient.GetAsync($"api/SolicitarDadosFpd?CoContratoAf={coContratoAF}&CoContratoAfDv={coContratoAFDV}");

    public async Task<List<RegistroDrp>> ObterRegistrosDrpAsync()
    {
        var lista = await _httpClient.GetFromJsonAsync<List<RegistroDrp>>("api/ObterRegistrosDrp");
        return lista ?? [];
    }

    public async Task<bool> BaixarDrpAsync(List<int> ids, string matriculaUsuario, string senha)
    {
        var req = new { Ids = ids, MatriculaUsuario = matriculaUsuario, Senha = senha };
        var resposta = await _httpClient.PutAsJsonAsync("api/BaixarDrp", req);
        return resposta.IsSuccessStatusCode;
    }
}
