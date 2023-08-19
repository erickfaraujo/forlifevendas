using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.LocaisVenda;

public class GetClientesRequestHandler : IRequestHandler<GetClientesRequest, Result<GetClientesResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILocalVendaRepository _localVendaRepository;

    public GetClientesRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILocalVendaRepository localVendaRepository)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _localVendaRepository = localVendaRepository;
    }

    public async Task<Result<GetClientesResponse>> Handle(GetClientesRequest request, CancellationToken cancellationToken)
    {
        var clientes = await _forlifeVendasRepository.GetAllAsync<Cliente>();

        if (clientes is null || !clientes.Any())
            return new ClienteNaoLocalizadoException();

        var totalClientes = clientes.Count();

        IEnumerable<Cliente> clientesResponse = new List<Cliente>(clientes);

        if (request.Nome is not null) clientesResponse = clientesResponse.Where(cliente => cliente.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase));
        if (request.Telefone is not null) clientesResponse = clientesResponse.Where(cliente => cliente.Telefone.Replace("-", "").Replace(" ", "").Contains(request.Telefone));
        if (request.IdLocal is not null) clientesResponse = clientesResponse.Where(cliente => cliente.IdLocal.Contains(request.IdLocal, StringComparison.OrdinalIgnoreCase));

        var clientesRetornados = clientesResponse.Count();
        var locais = await _localVendaRepository.GetAllAsync();

        foreach (var cliente in clientesResponse)
        {
            var local = locais!.FirstOrDefault(local => local.IdLocal == Guid.Parse(cliente.IdLocal));
            cliente.NomeLocal = local!.Descricao;
        }

        return clientesResponse is null || !clientesResponse.Any()
            ? new ClienteNaoLocalizadoException()
            : new GetClientesResponse(totalClientes, clientesRetornados, clientesResponse);
    }
}