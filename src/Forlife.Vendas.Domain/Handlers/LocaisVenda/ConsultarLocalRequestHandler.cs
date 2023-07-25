using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.LocaisVenda;
using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.LocaisVenda;

public class ConsultarLocalRequestHandler : IRequestHandler<ConsultarLocalRequest, Result<ConsultarLocalResponse>>
{
    private readonly ILocalVendaRepository _localVendaRepository;

    public ConsultarLocalRequestHandler(ILocalVendaRepository localVendaRepository)
        => _localVendaRepository = localVendaRepository;

    public async Task<Result<ConsultarLocalResponse>> Handle(ConsultarLocalRequest request, CancellationToken cancellationToken)
    {
        var local = await _localVendaRepository.GetAsync(Guid.Parse(request.Id));

        return local is null
            ? new LocalNaoLocalizadoException()
            : new ConsultarLocalResponse(local);
    }
}