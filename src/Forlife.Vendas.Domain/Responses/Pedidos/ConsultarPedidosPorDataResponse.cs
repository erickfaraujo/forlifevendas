using Forlife.Vendas.Domain.Models;

namespace Forlife.Vendas.Domain.Responses.Pedidos;

public record ConsultarPedidosPorDataResponse(IEnumerable<Pedido> Pedidos);