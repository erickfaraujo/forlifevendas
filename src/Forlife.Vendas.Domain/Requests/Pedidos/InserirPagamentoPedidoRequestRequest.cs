using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Pedidos;

public record InserirPagamentoPedidoRequestRequest(string IdPedido, DateTime Data, decimal Valor) : IRequest<Result<InserirPagamentoPedidoResponse>>;