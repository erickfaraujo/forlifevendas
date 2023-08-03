using Forlife.Vendas.Domain.Models;

namespace Forlife.Vendas.Domain.Repositories;

public interface ILocalVendaRepository
{

    Task<bool> CreateAsync(LocalVenda localVenda);

    Task<LocalVenda?> GetAsync(Guid Id);

    Task<IEnumerable<LocalVenda>?> GetAllAsync();

}
