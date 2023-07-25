using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Responses;
using MediatR;
using OperationResult;
using System.Net;

namespace Forlife.Vendas.Api.Extensions;

public static class Extensions
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> SendCommand<T>(this IMediator mediator, IRequest<Result<T>> request, Func<T, Microsoft.AspNetCore.Http.IResult>? result = null)
        => await mediator.Send(request) switch
        {
            (true, var response, _) => result != null ? result(response!) : Results.Ok(response),
            var (_, _, error) => HandleError(error!)
        };

    private static Microsoft.AspNetCore.Http.IResult HandleError(Exception error)
        => error switch
        {
            CadastrarClienteException e => new StatusCodeResult<ErroResponse>((int)HttpStatusCode.InternalServerError, new(e.Mensagem)),
            ClienteNaoLocalizadoException e => new StatusCodeResult<ErroResponse>((int)HttpStatusCode.NotFound, new(e.Mensagem)),
            CadastrarLocalException e => new StatusCodeResult<ErroResponse>((int)HttpStatusCode.InternalServerError, new(e.Mensagem)),
            LocalNaoLocalizadoException e => new StatusCodeResult<ErroResponse>((int)HttpStatusCode.BadRequest, new(e.Mensagem)),
            _ => new StatusCodeResult<ErroResponse>(500, new("Erro Genérico"))
        };

    private readonly record struct StatusCodeResult<T>(int StatusCode, T? Value) : Microsoft.AspNetCore.Http.IResult
    {
        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = StatusCode;
            return Value is null
                ? Task.CompletedTask
                : httpContext.Response.WriteAsJsonAsync(Value, Value.GetType());
        }
    }
}
