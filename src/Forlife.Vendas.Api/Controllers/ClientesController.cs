using Forlife.Vendas.Api.Extensions;
using Forlife.Vendas.Domain.Requests.Clientes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forlife.Vendas.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IResult> CadastrarCliente(CadastrarClienteRequest request)
        => await _mediator.SendCommand(request);

    [HttpGet("/{id}")]
    public async Task<IResult> ConsultarCliente(string id)
    {
        var request = new ConsultarClienteRequest(id);
        return await _mediator.SendCommand(request);
    }
}
