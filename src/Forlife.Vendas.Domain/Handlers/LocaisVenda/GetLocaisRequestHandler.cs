using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.LocaisVenda;
using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.LocaisVenda;

public class GetLocaisRequestHandler : IRequestHandler<GetLocaisRequest, Result<GetLocaisResponse>>
{
    private readonly ILocalVendaRepository _localVendaRepository;

    public GetLocaisRequestHandler(ILocalVendaRepository localVendaRepository)
    {
        _localVendaRepository = localVendaRepository;
    }

    public async Task<Result<GetLocaisResponse>> Handle(GetLocaisRequest request, CancellationToken cancellationToken)
    {
        var locais = await _localVendaRepository.GetAllAsync();

        if (locais is null) return new LocalNaoLocalizadoException();

        if (request.Descricao is not null) locais = locais.Where(cliente => cliente.Descricao.Contains(request.Descricao, StringComparison.OrdinalIgnoreCase));
        if (request.Endereco is not null) locais = locais.Where(cliente => cliente.Endereco.Replace("-", "").Replace(" ", "").Contains(request.Endereco));

        return locais is null || !locais.Any()
            ? new LocalNaoLocalizadoException()
            : new GetLocaisResponse(locais);
    }
}