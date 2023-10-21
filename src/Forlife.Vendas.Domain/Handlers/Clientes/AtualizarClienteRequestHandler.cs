using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class AtualizarClienteRequestHandler : IRequestHandler<AtualizarClienteRequest, Result<bool>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILocalVendaRepository _localVendaRepository;
    private readonly ILogger<AtualizarClienteRequestHandler> _logger;

    public AtualizarClienteRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILocalVendaRepository localVendaRepository, ILogger<AtualizarClienteRequestHandler> logger)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _localVendaRepository = localVendaRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(AtualizarClienteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando Atualização de cliente");

        var localVenda = await _localVendaRepository.GetAsync(request.IdLocalVenda);
        if (localVenda is null || string.IsNullOrEmpty(localVenda.Descricao))
            throw new LocalNaoLocalizadoException();

        var cliente = await _forlifeVendasRepository.GetAsync<Cliente>(request.Id, "PERFIL")
                      ?? throw new ClienteNaoLocalizadoException();

        cliente.Nome = request.Nome;
        cliente.Telefone = request.Telefone;
        cliente.Email = request.Email;
        cliente.DtNascimento = request.DataNascimento.ToString();
        cliente.IdLocal = request.IdLocalVenda.ToString();
        
        var resultInsert = await _forlifeVendasRepository.CreateAsync(cliente);

        return resultInsert
            ? Result.Success(true)
            : throw new AtualizarClienteException();
    }
}
