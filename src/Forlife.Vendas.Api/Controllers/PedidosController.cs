using Forlife.Vendas.Api.Extensions;
using Forlife.Vendas.Domain.Requests.Pedidos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forlife.Vendas.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PedidosController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IResult> CriarPedido(CriarPedidoRequest request)
        => await _mediator.SendCommand(request);

    [HttpGet("cliente/{idCliente}")]
    public async Task<IResult> ConsultarPedidoCliente(string idCliente, bool pagos)
        => await _mediator.SendCommand(new ConsultarPedidosClienteRequest(idCliente, pagos));

    [HttpGet("data")]
    public async Task<IResult> ConsultarPedidoCliente(string dataInicio, string dataFim)
        => await _mediator.SendCommand(new ConsultarPedidosPorDataRequest(dataInicio, dataFim));

}
