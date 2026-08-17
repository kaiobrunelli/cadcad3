using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PlataformaOperacional.Model.Plataforma;

public static class ServiceCollectionExtensions
{

	//public static IServiceCollection AddBaseAddress(this IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
	//{
	//	var ambiente = hostEnvironment.IsDevelopment();
	//	var baseHomolocao = ambiente ? "https://localhost:7001/" : "https://api.meuapp.com/";
	//	var baseLocalMock = hostEnvironment.BaseAddress;

	//	services.AddHttpClient("Producao", client =>
	//	{
	//		client.BaseAddress = new Uri(baseHomolocao);
	//	});
	//	services.AddHttpClient("Homologação", client =>
	//	{
	//		client.BaseAddress = new Uri(baseHomolocao);
	//	});
	//	return services;
	//}

	public static IServiceCollection AddProjectHttpClientsPlataforma(this IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
	{
	
		var isDev = hostEnvironment.IsDevelopment();


		var baseAddressPrincipal = isDev
			? "http://localhost:5079/" // ControleAnaliseDesembolso.Api local (CAD)
			: "https://www.ativo.fgts.caixa/PlataformaOperacional/";
		//: "https://www.ativo.fgts.caixa/PlataformaGestao/";  // Caso seja homologação


		var baseAddressLocalWwwRoot = hostEnvironment.BaseAddress;

	
		services.AddHttpClient("Api", client => {
			client.BaseAddress = new Uri(baseAddressPrincipal);		
		});

		services.AddHttpClient("ApiLocal", client => {
			client.BaseAddress = new Uri(baseAddressLocalWwwRoot);
		});

		return services;
	}

	
}
