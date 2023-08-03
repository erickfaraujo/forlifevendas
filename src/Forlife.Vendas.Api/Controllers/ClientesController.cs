using Forlife.Vendas.Api.Extensions;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Requests.LocaisVenda;
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

    [HttpGet("{id}")]
    public async Task<IResult> ConsultarCliente(string id)
        => await _mediator.SendCommand(new ConsultarClienteRequest(id));


    [HttpGet("clientesPorLoja/{idLocal}")]
    public async Task<IResult> ConsultarClientesPorLocal(string idLocal)
        => await _mediator.SendCommand(new ConsultarClientesPorLocalRequest(idLocal));

    [HttpGet]
    public async Task<IResult> Clientes()
        => await _mediator.SendCommand(new GetClientesRequest());
}
