using Forlife.Vendas.Domain.Models;

namespace Forlife.Vendas.Domain.Responses.LocaisVenda;

public record GetClientesResponse(int TotalClientes, int ClientesRetornados, IEnumerable<Cliente> Clientes);

