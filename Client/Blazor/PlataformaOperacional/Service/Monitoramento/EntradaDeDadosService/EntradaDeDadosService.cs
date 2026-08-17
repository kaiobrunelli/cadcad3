using PlataformaOperacional.Model.MonitoramentoModel;
using System.Net.Http.Json;

namespace PlataformaOperacional.Service.Monitoramento.EntradaDeDadosService;

public class EntradaDeDadosService
{
    private readonly HttpClient _httpClient;
    private readonly HttpClient _httpLocal;

    public EntradaDeDadosService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Api");
        _httpLocal = httpClientFactory.CreateClient("ApiLocal");
    }

    public async Task<HttpResponseMessage> IncluirDados(List<Entrada> entradas) =>
    await _httpClient.PostAsJsonAsync($"api/IncluirDados", entradas);

    public async Task<HttpResponseMessage> ConsultarUltimoControle() =>
        await _httpClient.GetAsync($"api/ConsultarUltimoControle");
}


