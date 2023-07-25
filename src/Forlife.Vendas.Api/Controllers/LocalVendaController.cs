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
    public async Task<IResult> CadastrarLocal(CadastrarLocalRequest request)
        => await _mediator.SendCommand(request);

    [HttpGet("{id}")]
    public async Task<IResult> ConsultarLocal(string id)
    {
        var request = new ConsultarLocalRequest(id);
        return await _mediator.SendCommand(request);
    }

    [HttpGet()]
    public async Task<IResult> Locais()
        => await _mediator.SendCommand(new GetLocaisRequest());
}
