using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Pedidos;

public record CriarPedidoRequest(IEnumerable<Item>? Itens, decimal ValorTotal, decimal ValorPago, string IdCliente, string? Observacoes, string CodProdutos, DateTime DataPedido) : IRequest<Result<CriarPedidoResponse>>;

public record Item(int CodigoProduto, int Quantidade);