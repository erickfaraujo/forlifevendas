using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Pedidos;

public record ConsultarPedidosPorDataRequest(string DataInicio, string DataFim) : IRequest<Result<ConsultarPedidosPorDataResponse>>;
