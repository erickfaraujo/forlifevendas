using Forlife.Vendas.Domain.Models;

namespace Forlife.Vendas.Domain.Repositories;

public interface IClienteRepository
{

    Task<bool> CreateAsync(Cliente cliente);

    Task<Cliente?> GetAsync(Guid Id);
}
