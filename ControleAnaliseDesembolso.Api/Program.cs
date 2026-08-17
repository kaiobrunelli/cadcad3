using ControleAnaliseDesembolso.Application;
using ControleAnaliseDesembolso.Application.Interface;
using ControleAnaliseDesembolso.Hubs;
using ControleAnaliseDesembolso.Infra.Data.Context;
using ControleAnaliseDesembolso.Interface;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaNotificacao.Application;
using PlataformaNotificacao.Application.Interface;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ControleAnaliseDesembolso")
    ?? throw new InvalidOperationException("Connection string 'ControleAnaliseDesembolso' não configurada.");

var connectionStringNotificacao = builder.Configuration.GetConnectionString("PlataformaNotificacao")
    ?? throw new InvalidOperationException("Connection string 'PlataformaNotificacao' não configurada.");

builder.Services.AddDbContext<ControleAnaliseDesembolsoContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IControleAnaliseDesembolso, ControleAnaliseDesembolsoService>();
builder.Services.AddScoped<IFichaPedidoDesembolsoService, FichaPedidoDesembolsoService>();
builder.Services.AddScoped<IValidadorDesembolsoService, ValidadorDesembolsoService>();
builder.Services.AddScoped<IEmpregadoCADService, EmpregadoCADService>();
// Fake enquanto não existe integração real com o sistema interno — trocar
// por uma implementação de verdade é só registrar outra classe aqui.
builder.Services.AddScoped<IValoresReferenciaContratoService, ValoresReferenciaContratoServiceFake>();

// PlataformaNotificacao é uma classlib à parte, com seu próprio contexto/banco
// (Notificacao/ControleVisualizacao) — NotificacaoService recebe a connection
// string direto no construtor (não via DbContextOptions<T>/AddDbContext).
builder.Services.AddScoped<INotificacaoService>(_ => new NotificacaoService(connectionStringNotificacao));

// Hub próprio (tempo real) — ChatHub/SignalRNotificacaoService espelham o
// SignalRService real (RedeCaixaUtilitario.Application), só que locais.
builder.Services.AddSignalR();
builder.Services.AddScoped<SignalRNotificacaoService>();

builder.Services.AddControllers()
    // Controllers vivem na classlib ControleAnaliseDesembolso, não neste host —
    // garante que sejam descobertos independentemente da heurística padrão.
    .AddApplicationPart(typeof(ControleAnaliseDesembolso.Controllers.DesembolsoController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Libera o front Blazor WASM (roda em outra porta) pra chamar esta API em dev.
const string CorsWasmDev = "CorsWasmDev";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsWasmDev, policy => policy
        .WithOrigins("http://localhost:5181", "https://localhost:7224")
        .AllowAnyHeader()
        .AllowAnyMethod()
        // SignalR precisa disso pro handshake/negotiate entre origens diferentes.
        .AllowCredentials());
});

var app = builder.Build();

// Qualquer exceção não tratada vira um ProblemDetails limpo (só o Detail com
// a mensagem) — antes disso, em Development o middleware padrão despejava o
// erro inteiro (stack trace, cabeçalhos da requisição etc.) direto na
// resposta, e o front acabava mostrando tudo isso cru pro usuário. Registrado
// bem no início do pipeline pra capturar exceções de qualquer controller.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var erro = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Erro ao processar a solicitação",
            Detail = erro?.Message,
        });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sem redirecionamento pra HTTPS em dev: o WASM chama sempre http://localhost:5079
// (fixo em ServiceCollectionExtensions.cs); um redirect http->https aqui quebra
// silenciosamente o fetch no navegador (CORS não é revalidado direito no redirect).
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(CorsWasmDev);

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
