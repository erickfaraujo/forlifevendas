using Forlife.Vendas.Api.Extensions;
using Forlife.Vendas.Domain.Requests.Resumo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forlife.Vendas.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ResumoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResumoController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> ResumoHome()
        => await _mediator.SendCommand(new GetResumoRequest());
}
