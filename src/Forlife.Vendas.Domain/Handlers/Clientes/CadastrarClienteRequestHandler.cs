using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class CadastrarClienteRequestHandler : IRequestHandler<CadastrarClienteRequest, Result<CadastrarClienteResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILocalVendaRepository _localVendaRepository;
    private readonly ILogger<CadastrarClienteRequestHandler> _logger;

    public CadastrarClienteRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILocalVendaRepository localVendaRepository, ILogger<CadastrarClienteRequestHandler> logger)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _localVendaRepository = localVendaRepository;
        _logger = logger;
    }
    public async Task<Result<CadastrarClienteResponse>> Handle(CadastrarClienteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando cadastro de cliente");

        var localVenda = await _localVendaRepository.GetAsync(request.IdLocalVenda);
        if (localVenda is null || string.IsNullOrEmpty(localVenda.Descricao))
            return new LocalNaoLocalizadoException();

        var cliente = new Cliente()
        {
            Nome = request.Nome,
            Telefone = request.Telefone,
            Email = request.Email,
            DtNascimento = request.DataNascimento.ToString("yyyy-MM-dd"),
            IdLocal = localVenda.IdLocal.ToString()
        };

        var cliExistente = await _forlifeVendasRepository.GetAsync<Cliente>(cliente.Pk, cliente.Sk);

        if (cliExistente is not null)
            return new Exception("Cliente já existe");

        var resultInsert = await _forlifeVendasRepository.CreateAsync(cliente);

        return resultInsert
            ? new CadastrarClienteResponse(cliente.Pk)
            : new CadastrarClienteException();
    }
}