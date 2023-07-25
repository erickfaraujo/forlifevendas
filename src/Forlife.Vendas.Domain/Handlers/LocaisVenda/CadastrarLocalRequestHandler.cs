using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.LocaisVenda;
using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.LocaisVenda;

public class CadastrarLocalRequestHandler : IRequestHandler<CadastrarLocalRequest, Result<CadastrarLocalResponse>>
{
    private readonly ILocalVendaRepository _localVendaRepository;
    private readonly ILogger<CadastrarLocalRequestHandler> _logger;

    public CadastrarLocalRequestHandler(ILocalVendaRepository localVendaRepository, ILogger<CadastrarLocalRequestHandler> logger)
    {
        _localVendaRepository = localVendaRepository;
        _logger = logger;
    }
    public async Task<Result<CadastrarLocalResponse>> Handle(CadastrarLocalRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando cadastro de Local de Venda");

        var localVenda = new LocalVenda()
        {
            Id = Guid.NewGuid(),
            Descricao = request.Descricao,
            Endereco = request.Endereco,
            Referencia = request.Referencia
        };

        var resultInsert = await _localVendaRepository.CreateAsync(localVenda);

        return resultInsert
            ? new CadastrarLocalResponse(localVenda.Id)
            : new CadastrarLocalException();
    }
}