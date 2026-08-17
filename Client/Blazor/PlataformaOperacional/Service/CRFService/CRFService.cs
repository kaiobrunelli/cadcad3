using PlataformaOperacional.Model.CentralPermissoes;
using PlataformaOperacional.Model.CRF;
using System.Net.Http.Json;

namespace PlataformaOperacional.Service.CRFService
{
	public class CRFService
	{
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpLocal;
    
        public CRFService(IHttpClientFactory httpClientFactory)
        {          
            _httpClient = httpClientFactory.CreateClient("Api");        
            _httpLocal = httpClientFactory.CreateClient("ApiLocal");    
        }
       

		public async Task<HttpResponseMessage> RealizarOperacaoCrf(SenhaUsuario senhaCriptografada)
		{
			var response = await _httpClient.PostAsJsonAsync($"api/RealizarOperacaoCrf", senhaCriptografada);
			return response;
		}

        public async Task<HttpResponseMessage> ConsultarCrfAtual()
        {
            var response = await _httpClient.GetAsync($"api/ConsultarCrfAtual");
            return response;
        }

        public async Task<HttpResponseMessage> ConsultarCrfAnteriores(DateTime? dataConsulta)
        {
			if (dataConsulta is null) throw new ArgumentNullException("Data da consulta inválida.");
            var response = await _httpClient.GetAsync($"api/ConsultarCrfAnteriores?data={dataConsulta.Value.ToString("yyyy-MM-dd")}");
            return response;
        }
    }
}
