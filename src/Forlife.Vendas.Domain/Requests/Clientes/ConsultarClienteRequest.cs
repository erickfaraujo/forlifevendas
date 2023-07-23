using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Clientes;

public record ConsultarClienteRequest(string Id) : IRequest<Result<ConsultarClienteResponse>>;

