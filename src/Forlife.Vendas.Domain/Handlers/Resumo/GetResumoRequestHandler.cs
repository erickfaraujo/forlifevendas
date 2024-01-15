using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Resumo;
using Forlife.Vendas.Domain.Responses.Resumo;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Resumo;

public class GetResumoRequestHandler : IRequestHandler<GetResumoRequest, Result<GetResumoResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;

    public GetResumoRequestHandler(IForlifeVendasRepository forlifeVendasRepository)
        => _forlifeVendasRepository = forlifeVendasRepository;

    public Task<Result<GetResumoResponse>> Handle(GetResumoRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
