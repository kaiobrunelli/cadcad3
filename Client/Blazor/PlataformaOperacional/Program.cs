using ControleAnaliseDesembolso.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using PlataformaNotificacao.UI.Servicos;
using PlataformaOperacional;
using PlataformaOperacional.Model.Plataforma;
using PlataformaOperacional.Service;
using PlataformaOperacional.Service.AplicacaoService;
using PlataformaOperacional.Service.AplicacaoService.Preditor;
using PlataformaOperacional.Service.Cobranca;
using PlataformaOperacional.Service.Cobranca.EncontroDeContas;
using PlataformaOperacional.Service.Contabilidade;
using PlataformaOperacional.Service.ContratacaoService;
using PlataformaOperacional.Service.CRFService;
using PlataformaOperacional.Service.Middleware;
using PlataformaOperacional.Service.Monitoramento.EntradaDeDadosService;
using SipubDesembolsos.Client.Servicos;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Configuration.AddJsonFile("appsettings.json",optional:false,reloadOnChange:true);
builder.Configuration.AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json",optional:true,reloadOnChange:true);
var baseHref = builder.Configuration.GetValue<string>("BasePath");
builder.Services.AddMudServices();
// Em produção, app e API ficam sob o mesmo domínio (reverse proxy) — por
// isso BaseAddress do próprio host funciona pro HttpClient padrão. Em dev
// local eles rodam em portas diferentes, então precisa apontar pra API de
// verdade (mesma URL do client nomeado "Api" em AddProjectHttpClientsPlataforma) —
// senão qualquer componente que injeta HttpClient puro (em vez do nomeado)
// chama a própria origem do WASM e falha (404/405).
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.IsDevelopment()
        ? "http://localhost:5079/"
        : builder.HostEnvironment.BaseAddress)
});

string baseAdress = "";



//var baseAddressApi = builder.HostEnvironment.IsDevelopment()
//	? "https://localhost:7211/"
//	: "https://www.ativo.fgts.caixa/PlataformaOperacional/";
////: "https://www.ativo.fgts.caixa/PlataformaGestao/";  // Caso seja homologação

builder.Services.AddProjectHttpClientsPlataforma(builder.HostEnvironment);

//builder.Services.AddScoped(sp => new HttpClient
//{
//	BaseAddress = new Uri(baseAddressApi)
//});

//builder.Services.AddHttpClient("BackendApi", client =>
//{
//	client.BaseAddress = new Uri(baseAddressApi);
//	client.Timeout = TimeSpan.FromSeconds(60);
//});






builder.Services.Configure<SimulacaoA>(builder.Configuration.GetSection("SimulacaoA"));
builder.Services.Configure<SimulacaoB>(builder.Configuration.GetSection("SimulacaoB"));
builder.Services.AddSingleton<MudThemeService>();
builder.Services.AddSingleton(provider => new BlazorMockService(false));
//builder.Services.AddSingleton(provider => new SignalRService(baseAddressApi));
builder.Services.AddSingleton<SignalRService>();
builder.Services.AddScoped<DialogServicePlataformaOperacional>();
builder.Services.AddScoped<ContabilidadeService>();
builder.Services.AddScoped<PlataformaService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ContratacaoService>();   
builder.Services.AddScoped<CRFService>();   
builder.Services.AddScoped<EncryptionService>();   
builder.Services.AddScoped<SaldoResidualService>();
builder.Services.AddScoped<CobrancaService>();
builder.Services.AddScoped<PreditorService>();
builder.Services.AddScoped<DownloadService>();
builder.Services.AddScoped<DesembolsosService>();
builder.Services.AddScoped<EntradaDeDadosService>();
builder.Services.AddScoped<EncontroDeContasService>();
builder.Services.AddScoped<ServicoUsuario>();
//builder.Services.AddScoped<ServicoNotificacaoHub>();
builder.Services.AddSingleton<ServicoNotificacao>();
builder.Services.AddSingleton<NotificacaoServiceUI>();
builder.Services.AddScoped<ControleAnaliseDesembolsoService>();



//builder.Logging.SetMinimumLevel(LogLevel.Debug);
//builder.Services.AddScoped<AuthenticationStateProvider>();
//builder.Services.AddAuthorizationCore();

var urlServidor = builder.Configuration["ServidorUrl"] ?? "https://localhost:7211";
builder.Services.AddSingleton<ServicoUsuarioUI>();
builder.Services.AddSingleton<NotificacaoServiceSignalRUI>();
builder.Services.AddSingleton( _ => new SignalRServiceUI(urlServidor));


//builder.Services.AddHttpClient("FrontClient", client =>
//{
//	var teste = builder.HostEnvironment.BaseAddress;
//	client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
//});


//builder.Services.AddHttpClient("ApiClientHomologacao", client =>
//{
//	client.BaseAddress = new Uri("https://www.ativo.fgts.caixa/PlataformaGestao/");
//});
//builder.Services.AddHttpClient("ApiClientProducao", client =>
//{
//	client.BaseAddress = new Uri("https://www.ativo.fgts.caixa/PlataformaOperacional/");
//});


await builder.Build().RunAsync();


