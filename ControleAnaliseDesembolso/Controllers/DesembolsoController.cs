using ControleAnaliseDesembolso.Application.Dtos.Request;
using ControleAnaliseDesembolso.Application.Dtos.Response;
using ControleAnaliseDesembolso.Application.Interface;
using ControleAnaliseDesembolso.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ControleAnaliseDesembolso.Controllers
{
    [ApiController]
    [Route("/api/[action]")]
    public class DesembolsoController : ControllerBase
    {
        private readonly IControleAnaliseDesembolso _servico;
        private readonly IFichaPedidoDesembolsoService _servicoFpd;

        public DesembolsoController(
            IControleAnaliseDesembolso servico,
            IFichaPedidoDesembolsoService servicoFpd)
        {
            _servico = servico;
            _servicoFpd = servicoFpd;
        }


        [HttpGet]
        public async Task<ActionResult<List<DesembolsoResponse>>> ObterTodosDesembolsos(CancellationToken cancellationToken)
            => Ok(await _servico.ObterTodosDesembolsos(cancellationToken));

        [HttpGet("{coDesembolso}")]
        public async Task<ActionResult<DesembolsoDetalheResponse>> ObterDetalheDesembolso(int coDesembolso, CancellationToken cancellationToken)
            => Ok(await _servico.ObterDetalheDesembolso(coDesembolso, cancellationToken));

        [HttpGet("{coDesembolso}/comentarios")]
        public async Task<ActionResult<List<ComentarioValidacaoResponse>>> ObterComentarios(int coDesembolso, CancellationToken cancellationToken)
            => Ok(await _servico.ObterComentarios(coDesembolso, cancellationToken));

        [HttpPost]
        public async Task<IActionResult> AdicionarComentario(
            [FromQuery] int coDesembolso, [FromQuery] int coValidacao, [FromBody] ValidacaoRegistroRequest registro, CancellationToken cancellationToken)
        {
            await _servico.AdicionarComentario(new ValidacaoDesembolsoRequest
            {
                CoDesembolso = coDesembolso,
                CoValidacao = coValidacao,
                ValidacaoRegistro = registro,
            }, cancellationToken);
            return Ok();
        }

        [HttpPut("{comentarioId}")]
        public async Task<IActionResult> EditarComentario(int comentarioId, [FromBody] EditarComentarioRequest request, CancellationToken cancellationToken)
        {
            request.CoRegistroValidacao = comentarioId;
            await _servico.EditarComentario(request, cancellationToken);
            return Ok();
        }

        [HttpPut("{coComentario}")]
        public async Task<IActionResult> RemoverComentario(int coComentario, [FromQuery] string matriculaSolicitante, CancellationToken cancellationToken)
        {
            await _servico.RemoverComentario(coComentario, matriculaSolicitante, cancellationToken);
            return Ok();
        }

        [HttpPut("{coDesembolso}")]
        public async Task<IActionResult> Aprovar(int coDesembolso, [FromBody] AprovarDesembolsoRequest request, CancellationToken cancellationToken)
        {
            await _servico.AprovarDesembolso(coDesembolso, request, cancellationToken);
            return Ok();
        }

        [HttpPut("{coDesembolso}")]
        public async Task<IActionResult> BaixarDRP(int coDesembolso, CancellationToken cancellationToken)
        {
            await _servico.BaixarDRP(coDesembolso, cancellationToken);
            return Ok();
        }

        [HttpPut("{coDesembolso}")]
        public async Task<IActionResult> Rejeitar(int coDesembolso, [FromBody] RejeitarDesembolsoRequest request, CancellationToken cancellationToken)
        {
            await _servico.RejeitarDesembolso(coDesembolso, request, cancellationToken);
            return Ok();
        }

        [HttpPut("{coDesembolso}")]
        public async Task<IActionResult> VincularResponsavel(int coDesembolso, [FromBody] string? matriculaResponsavel, CancellationToken cancellationToken)
        {
            await _servico.VincularResponsavel(coDesembolso, matriculaResponsavel, cancellationToken);
            return Ok();
        }

        [HttpPut("{coDesembolso}")]
        public async Task<IActionResult> RemoverResponsavel(int coDesembolso, CancellationToken cancellationToken)
        {
            await _servico.VincularResponsavel(coDesembolso, null, cancellationToken);
            return Ok();
        }

        [HttpPost("{coDesembolso}")]
        public async Task<IActionResult> Validar(int coDesembolso, CancellationToken cancellationToken)
        {
            await _servico.ValidarDesembolso(coDesembolso, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ValidarTodosPendentes(CancellationToken cancellationToken)
        {
            await _servico.ValidarTodosPendentes(cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<ValidacaoTemplateResponse>>> ObterValidacoesTemplate(CancellationToken cancellationToken)
            => Ok(await _servico.ObterValidacoesTemplate(cancellationToken));

        [HttpGet]
        public async Task<ActionResult<PedidoConsultaContratoAfResponse>> SolicitarDadosFpd(
            [FromQuery] PedidoConsultaContratoAfRequest pedido, CancellationToken cancellationToken)
            => Ok(await _servicoFpd.SolicitarDadosFPD(pedido, cancellationToken));

        [HttpPost]
        public async Task<IActionResult> CriarFichaPedidoDesembolso([FromBody] PedidoDesembolsoRequest request, CancellationToken cancellationToken)
        {
            await _servico.CriarFichaPedidoDesembolso(request, cancellationToken);
            return Ok();
        }

        [HttpPut("{coFpd}")]
        public async Task<IActionResult> ReenviarFicha(int coFpd, [FromBody] PedidoDesembolsoRequest request, CancellationToken cancellationToken)
        {
            await _servico.ReenviarFichaPedidoDesembolso(coFpd, request, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<RegistroDrpResponse>>> ObterRegistrosDrp(CancellationToken cancellationToken)
            => Ok(await _servico.ObterRegistrosDrp(cancellationToken));

        [HttpPut]
        public async Task<IActionResult> BaixarDrp([FromBody] BaixarDrpRequest request, CancellationToken cancellationToken)
        {
            await _servico.BaixarDrpEmLote(request, cancellationToken);
            return Ok();
        }
    }
}
