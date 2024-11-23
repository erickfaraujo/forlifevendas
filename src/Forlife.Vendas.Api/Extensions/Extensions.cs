using Forlife.Vendas.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;

namespace Forlife.Vendas.Api.Extensions;

public static class Extensions
{
    public static async Task<IActionResult> SendCommand<T>(this IMediator mediator, IRequest<Result<T>> request)
        => await mediator.Send(request) switch
        {
            (true, var response, _) => new OkObjectResult(response),
            var (_, _, error) => HandleError(error!)
        };

    private static IActionResult HandleError(Exception error)
        => error switch
        {
            CadastrarClienteException e => new ObjectResult(e.Mensagem) { StatusCode = 500 },
            ClienteNaoLocalizadoException => new NotFoundResult(),
            CadastrarLocalException e => new ObjectResult(e.Mensagem) { StatusCode = 500 },
            LocalNaoLocalizadoException => new NotFoundResult(),
            PedidoNaoLocalizadoException e => new NotFoundObjectResult(e.Mensagem),
            AtualizarClienteException e => new ObjectResult(e.Mensagem) { StatusCode = 500 },
            ExcluirClienteException e => new ObjectResult(e.Mensagem) { StatusCode = 500 },
            ExcluirClienteComPedidoException e => new BadRequestObjectResult(e.Mensagem),
            _ => new ObjectResult(500)
        };
}
