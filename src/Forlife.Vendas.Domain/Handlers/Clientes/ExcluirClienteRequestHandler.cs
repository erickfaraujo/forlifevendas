using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class ExcluirClienteRequestHandler : IRequestHandler<ExcluirClienteRequest, Result<bool>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILogger<ExcluirClienteRequestHandler> _logger;

    public ExcluirClienteRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILogger<ExcluirClienteRequestHandler> logger)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(ExcluirClienteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando Exclusão de cliente");

        _ = await _forlifeVendasRepository.GetAsync<Cliente>(request.Id, "PERFIL")
                      ?? throw new ClienteNaoLocalizadoException();
        
        var result = await _forlifeVendasRepository.DeleteAsync<Cliente>(request.Id, "PERFIL");

        return result
            ? Result.Success(true)
            : throw new ExcluirClienteException();
    }
}
