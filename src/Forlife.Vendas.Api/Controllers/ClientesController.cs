using Forlife.Vendas.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forlife.Vendas.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ClientesController : Controller
{
    private readonly IMediator _mediator;

    public ClientesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CadastrarCliente(CadastrarClienteRequest request)
        => Ok(await _mediator.Send(request));

    [HttpGet("/{id}")]
    public async Task<IActionResult> ConsultarCliente(string id)
    {
        var request = new ConsultarClienteRequest(id);
        return Ok(await _mediator.Send(request));
    }
        

}
