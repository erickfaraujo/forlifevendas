using Forlife.Vendas.Domain.Models;

namespace Forlife.Vendas.Domain.Repositories;

public interface IForlifeVendasRepository
{
    Task<bool> CreateAsync<T>(T obj) where T : class;

    Task<T?> GetAsync<T>(string pk, string sk) where T : class;

    Task<Pedido?> GetPedidoByIdAsync(string sk);

    Task<IEnumerable<Cliente>?> GetClienteByIdLocalAsync(Guid Id);

    Task<IEnumerable<T>?> GetAllAsync<T>() where T : class;

    Task<List<Pedido>?> GetPedidosClienteAsync(string pk);

    Task<List<Pedido>?> GetPedidosPorDataAsync(string dataInicio, string dataFim);
}
