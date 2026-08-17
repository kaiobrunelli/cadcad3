using Microsoft.AspNetCore.Mvc;
using PlataformaNotificacao.Application.Interface;

namespace ControleAnaliseDesembolso.Controllers
{
    // Rotas exatas esperadas pelo NotificacaoServiceUI (Modulos.Client/
    // PlataformaNotificacao.UI/Servicos/NotificacaoServiceUI.cs), consumido
    // pelo sino real (Client/Blazor/PlataformaOperacional/Layout/
    // SinoNotificacoes.razor) — não segue a convenção [action] do resto da
    // API porque os caminhos têm segmentos fixos (nao-lidas/total, etc.).
    [ApiController]
    [Route("api/notificacao")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _servico;

        public NotificacaoController(INotificacaoService servico)
        {
            _servico = servico;
        }

        [HttpGet("nao-lidas/total")]
        public async Task<ActionResult<int>> ContarNaoLidas([FromQuery] string usuarioId, CancellationToken cancellationToken)
            => Ok(await _servico.ContarNaoLidasAsync(usuarioId, cancellationToken));

        [HttpGet("matricula")]
        public async Task<ActionResult<List<NotificacaoParaSinoResponse>>> ObterPorMatricula([FromQuery] string matricula, CancellationToken cancellationToken)
        {
            var lista = await _servico.ObterNotificacaoPorMatriculaAsync(matricula, cancellationToken: cancellationToken);

            return Ok(lista.Select(n => new NotificacaoParaSinoResponse
            {
                CodigoNotificacao = n.CodigoNotificacao,
                CodigoAplicativo = (int?)n.CodigoAplicativo,
                Titulo = n.Titulo,
                Mensagem = n.Mensagem,
                Tipo = (int)n.Tipo,
                DataCriacao = n.DataCriacao,
                DataValidade = n.DataValidade,
                DataVisualizacao = n.DataVisualizacao,
                Link = n.Link,
            }));
        }

        [HttpPut("{codigoNotificacao}/lida")]
        public async Task<IActionResult> MarcarLida(int codigoNotificacao, [FromQuery] string usuarioId, CancellationToken cancellationToken)
        {
            await _servico.MarcarLidaAsync(codigoNotificacao, usuarioId, cancellationToken);
            return Ok();
        }

        [HttpPut("marcar-todas-lidas")]
        public async Task<IActionResult> MarcarTodasLidas([FromQuery] string usuarioId, CancellationToken cancellationToken)
        {
            await _servico.MarcarTodasLidasAsync(usuarioId, cancellationToken);
            return Ok();
        }
    }

    // Formato esperado pelo cliente (Plataforma.UI.Shared.Model.Notificacao) —
    // Tipo já vem remapeado pro enum do lado do front.
    public class NotificacaoParaSinoResponse
    {
        public int CodigoNotificacao { get; set; }
        public int? CodigoAplicativo { get; set; }
        public string Titulo { get; set; } = "";
        public string Mensagem { get; set; } = "";
        public int Tipo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public DateTime? DataVisualizacao { get; set; }
        public string? Link { get; set; }
    }
}
