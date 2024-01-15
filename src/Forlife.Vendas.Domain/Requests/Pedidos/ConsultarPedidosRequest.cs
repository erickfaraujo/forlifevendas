using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Pedidos;

public record ConsultarPedidosRequest(string? DataInicio, string? DataFim, string? StatusPagamento, string? IdLocal) : IRequest<Result<ConsultarPedidosResponse>>;
