using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class GetAniversariantesRequestHandler : IRequestHandler<GetAniversariantesRequest, Result<GetAniversariantesResponse>>
{
    private readonly ILogger<GetAniversariantesRequestHandler> _logger;
    private readonly IForlifeVendasRepository _forlifeVendasRepository;

    public GetAniversariantesRequestHandler(ILogger<GetAniversariantesRequestHandler> logger, IForlifeVendasRepository forlifeVendasRepository)
    {
        _logger = logger;
        _forlifeVendasRepository = forlifeVendasRepository;
    }

    public async Task<Result<GetAniversariantesResponse>> Handle(GetAniversariantesRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando a consulta de aniversariantes para o período {inicio} à {fim}", request.dataInicio, request.DataFim);

        var clientes = await _forlifeVendasRepository.GetAniversariantesAsync(request.dataInicio, request.DataFim);

        _logger.LogInformation("Consulta de aniversariantes realizada com sucesso");

        return new GetAniversariantesResponse(clientes);
    }
}
