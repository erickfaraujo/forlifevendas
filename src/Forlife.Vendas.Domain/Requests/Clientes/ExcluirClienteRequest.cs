using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Clientes;

public record ExcluirClienteRequest(string Id) : IRequest<Result<bool>>;
