using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Pedidos;

public record CriarPedidoRequest(string Itens, decimal ValorTotal, decimal ValorPago, string IdCliente) : IRequest<Result<CriarPedidoResponse>>;