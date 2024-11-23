using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Pedidos;

public record EditarPedidoRequest(string IdPedido, decimal ValorTotal, string Observacoes, string CodProdutos) : IRequest<Result<EditarPedidoResponse>>;