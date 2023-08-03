using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.LocaisVenda;
using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.LocaisVenda;

public class GetClientesRequestHandler : IRequestHandler<GetClientesRequest, Result<GetClientesResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;

    public GetClientesRequestHandler(IForlifeVendasRepository forlifeVendasRepository)
        =>_forlifeVendasRepository = forlifeVendasRepository;

    public async Task<Result<GetClientesResponse>> Handle(GetClientesRequest request, CancellationToken cancellationToken)
    {
        var clientes = await _forlifeVendasRepository.GetAllAsync<Cliente>();

        return clientes is null || !clientes.Any()
            ? new ClienteNaoLocalizadoException()
            : new GetClientesResponse(clientes);
    }
}