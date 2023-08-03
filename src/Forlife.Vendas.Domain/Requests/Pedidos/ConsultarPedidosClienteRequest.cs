using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Pedidos;

public record ConsultarPedidosClienteRequest(string IdCliente, bool Pagos) : IRequest<Result<ConsultarPedidosClienteResponse>>;
