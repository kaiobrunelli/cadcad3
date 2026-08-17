
using PlataformaOperacional.Model.Aplicacao.Preditor;
using PlataformaOperacional.Model.Plataforma;
using PlataformaOperacional.Service.Middleware;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace PlataformaOperacional.Service.AplicacaoService.Preditor
{
	public class PreditorService
	{
	
		private readonly HttpClient _httpClient;
		private readonly HttpClient _httpLocal;
		private readonly BlazorMockService _mockBlazor;

		public PreditorService(IHttpClientFactory httpClientFactory, BlazorMockService blazorMockService)
		{
			//_httpClient = httpClientFactory.CreateClient(ClientName);
			_httpClient = httpClientFactory.CreateClient("Api");

			_httpLocal = httpClientFactory.CreateClient("ApiLocal");				
			_mockBlazor = blazorMockService;
		}

	
		public async Task<HttpResponseMessage> ConsultarDadosDoPreditorDeDesembolso()
		{
			
			if (_mockBlazor.MockarDados)
			{
				var repsonseMock = await _httpLocal.GetAsync("sample-data/Preditor/ControlePreditorDeDesembolso.json");
				return repsonseMock;
			}
			var response = await _httpClient.GetAsync("api/ConsultarDadosDoPreditorDeDesembolso");
			return response;
		}
		public async Task<HttpResponseMessage> ConsultarPreditorAnaliticoPublico()
		{
			var response = await _httpClient.GetAsync("api/ConsultarPreditorAnaliticoPublico");
			return response;
		}
		public async Task<HttpResponseMessage> ConsultarPreditorAnaliticoPrivado()
		{
			var response = await _httpClient.GetAsync("api/ConsultarPreditorAnaliticoPrivado");
			return response;
		}
        public async Task<HttpResponseMessage> ConsultarRelatorioDeResultados()
        {
            var response = await _httpClient.GetAsync("api/ConsultarRelatorioDeResultados");
            return response;
        }
		
		public async Task<HttpResponseMessage> AtualizarPreditorAnaliticoPrivado(AnaliticoSetorPrivado? valor)
		{
			var response = await _httpClient.PostAsJsonAsync("api/AtualizarPreditorAnaliticoPrivado", valor);
			return response;
		}
		public async Task<HttpResponseMessage> AtualizarPreditorAnaliticoPublico(AnaliticoSetorPublico? valor)
		{
			var response = await _httpClient.PostAsJsonAsync("api/AtualizarPreditorAnaliticoPublico", valor);
			return response;
		}
		public async Task<HttpResponseMessage> ResetarPreditorAnaliticoPrivado()
		{
			var response = await _httpClient.PostAsync("api/ResetarPreditorAnaliticoPrivado", null);
			return response;
		}
		public async Task<HttpResponseMessage> ResetarPreditorAnaliticoPublico()
		{
			var response = await _httpClient.PostAsync("api/ResetarPreditorAnaliticoPublico", null);
			return response;
		}
		public async Task<HttpResponseMessage> FinalizarPreditorEnviarEmail()
		{
			var response = await _httpClient.PostAsync("api/FinalizarPreditorEnviarEmail", null);
			return response;
		}
		public async Task<HttpResponseMessage> ConsultarPreditorAutorizados()
		{
			HttpResponseMessage response;
			if (_mockBlazor.MockarDados)
			{
				response = await _httpLocal.GetAsync("sample-data/Preditor/PreditorAutorizados.json");
				return response;
			}

			response = await _httpClient.GetAsync("api/ConsultarPreditorAutorizados");
			return response;
		}

		public async Task<HttpResponseMessage> CadastrarPreditorAutorizado(CreatePreditorAutorizados cadastro)
		{
			var response = await _httpClient.PostAsJsonAsync("api/CadastrarPreditorAutorizado", cadastro);
			return response;
		}
		public async Task<HttpResponseMessage> DesabilitarPreditorAutorizado(string matricula)
		{
			var response = await _httpClient.PostAsJsonAsync("api/DesabilitarPreditorAutorizado", matricula);
			return response;
		}

		public async Task<HttpResponseMessage> CadastrarEventual(string matricula)
		{
			var response = await _httpClient.PostAsJsonAsync("api/CadastrarEventual", matricula);
			return response;
		}
		public async Task<HttpResponseMessage> RemoverEventual(string matricula)
		{
			var response = await _httpClient.PostAsJsonAsync("api/RemoverEventual", matricula);
			return response;
		}
		public async Task<HttpResponseMessage> ConsultarEmailPreditor()
		{
			HttpResponseMessage response;
			if (_mockBlazor.MockarDados)
			{
				response = await _httpLocal.GetAsync("sample-data/Preditor/EmailEmail.json");
				return response;
			}

			response = await _httpClient.GetAsync("api/ConsultarEmailPreditor");
			return response;
		}
		public async Task<HttpResponseMessage> CadastrarEmailPreditor(CreateDestinatarioEmailPreditor destinatarioEmail)
		{
			var response = await _httpClient.PostAsJsonAsync("api/CadastrarEmailPreditor", destinatarioEmail);
			return response;
		}

		public async Task<HttpResponseMessage> RemoverEmailPreditor(int coDestinatario)
		{
			var response = await _httpClient.PostAsJsonAsync("api/RemoverEmailPreditor", coDestinatario);
			return response;
		}



		//public async Task<HistoricoPreditor?> ConsultarUltimoDesembolso()
		//{
		//	// Adicionei "api/preditor/" antes
		//	return await _httpClient.GetFromJsonAsync<HistoricoPreditor?>("api/preditor/ultimo-desembolso");
		//}

		//public async Task<HttpStatusCode> AtualizarDesembolsoAsync(int id, AtualizarDesembolsoRequest request) // Ajuste o nome da classe se for AtualizarValorRequest
		//{
		//	var response = await _httpClient.PutAsJsonAsync($"api/preditor/atualizar-desembolso/{id}", request);

		//	if (!response.IsSuccessStatusCode)
		//	{
		//		var erro = await response.Content.ReadAsStringAsync();
		//		throw new Exception($"Erro ao atualizar: {erro}");
		//	}
		//	return response.StatusCode;
		//}

		//public async Task FinalizarDesembolsoAsync(int id)
		//{
		//	var response = await _httpClient.PostAsJsonAsync($"api/preditor/finalizar-desembolso/{id}", "");
		//	if (!response.IsSuccessStatusCode)
		//	{
		//		var erro = await response.Content.ReadAsStringAsync();
		//		throw new Exception($"Erro ao finalizar: {erro}");
		//	}
		//}

		//public async Task EditarDesembolsoAsync(int id, string tipo)
		//{
		//	// CORREÇÃO CRÍTICA: Passando via Query String para bater com o [FromQuery] do controller
		//	// Rota: api/preditor/editar-desembolso/1?tipo=publico
		//	var response = await _httpClient.PatchAsync($"api/preditor/editar-desembolso/{id}?tipo={tipo}", null);

		//	if (!response.IsSuccessStatusCode)
		//	{
		//		var erro = await response.Content.ReadAsStringAsync();
		//		throw new Exception($"Erro ao editar: {erro}");
		//	}
		//}
		//public async Task CriarPerfil(CriarPerfilAcessoRequest request)
		//{
		//	var response = await _httpClient.PostAsJsonAsync("api/preditor/criar-perfil-de-acesso", request);
		//	if (!response.IsSuccessStatusCode)
		//	{
		//		var erro = await response.Content.ReadAsStringAsync();
		//		throw new Exception($"Erro ao criar perfil: {erro}");
		//	}
		//}

		//public async Task<List<PerfilAcesso>> BuscarPerfisAcesso()
		//{
		//	var response = await _httpClient.GetFromJsonAsync<List<PerfilAcesso>>("api/preditor/buscar-todos-perfis-de-acesso");
		//	return response;
		//}

		//public async Task RemoverPerfil(string matricula)
		//{
		//	var response = await _httpClient.PostAsJsonAsync($"api/preditor/remover-perfil/{matricula}", "");
		//}
		//public async Task<HttpResponseMessage> AtualizarPerfil(AtualizarPerfiDto request)
		//{
		//	var response = await _httpClient.PatchAsJsonAsync($"api/preditor/atualizar-perfil/", request);
		//	return response;

		//}

		//public async Task<ControleGeralDto> ConsultarControleGeral(string matricula)
		//{
		//	var response = await _httpClient.GetFromJsonAsync<ControleGeralDto>($"api/preditor/controle-geral/{matricula}");
		//	if (response is null) throw new Exception("´Matrícula não localizada");
		//	return response;
		//}
	}
}

