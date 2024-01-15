using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Pedidos;

public record ConsultarDetalhesPedidoRequest(string IdPedido) : IRequest<Result<ConsultarDetalhesPedidoResponse>>;
