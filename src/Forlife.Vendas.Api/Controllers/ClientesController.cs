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
    public async Task<IActionResult> CadastrarCliente(CadastrarClienteRequest request)
        => await _mediator.SendCommand(request);

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarCliente(string id)
        => await _mediator.SendCommand(new ConsultarClienteRequest(id));


    [HttpGet("clientesPorLoja/{idLocal}")]
    public async Task<IActionResult> ConsultarClientesPorLocal(string idLocal)
        => await _mediator.SendCommand(new ConsultarClientesPorLocalRequest(idLocal));

    [HttpGet]
    public async Task<IActionResult> Clientes(string? nome, string? telefone, string? idLocal)
        => await _mediator.SendCommand(new GetClientesRequest(nome, telefone, idLocal));

    [HttpPut]
    public async Task<IActionResult> AlterarCliente(AtualizarClienteRequest request)
        => await _mediator.SendCommand(request);
}
