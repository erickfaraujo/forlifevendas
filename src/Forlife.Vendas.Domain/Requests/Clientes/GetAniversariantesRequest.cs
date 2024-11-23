using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Clientes;

public record GetAniversariantesRequest(DateTime dataInicio, DateTime DataFim) : IRequest<Result<GetAniversariantesResponse>>;