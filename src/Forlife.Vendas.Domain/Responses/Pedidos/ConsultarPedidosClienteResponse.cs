using Forlife.Vendas.Domain.Models;

namespace Forlife.Vendas.Domain.Responses.Pedidos;

public record ConsultarPedidosClienteResponse(IEnumerable<Pedido> Pedidos);