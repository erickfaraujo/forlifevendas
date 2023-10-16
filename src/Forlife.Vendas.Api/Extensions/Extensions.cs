﻿using Forlife.Vendas.Domain.Exceptions;
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
            ClienteNaoLocalizadoException e => new BadRequestObjectResult(e.Mensagem),
            CadastrarLocalException e => new ObjectResult(e.Mensagem) { StatusCode = 500 },
            LocalNaoLocalizadoException e => new BadRequestObjectResult(e.Mensagem),
            PedidoNaoLocalizadoException e => new BadRequestObjectResult(e.Mensagem),
            AtualizarClienteException e => new ObjectResult(e.Mensagem) { StatusCode = 500 },
            _ => new ObjectResult(500)
        };
}
