using Forlife.Vendas.Api.Extensions;
using Forlife.Vendas.Domain.Requests.LocaisVenda;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forlife.Vendas.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class LocalVendaController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocalVendaController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CadastrarLocal(CadastrarLocalRequest request)
        => await _mediator.SendCommand(request);

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarLocal(string id)
        => await _mediator.SendCommand(new ConsultarLocalRequest(id));

    [HttpGet()]
    public async Task<IActionResult> Locais()
        => await _mediator.SendCommand(new GetLocaisRequest());
}
