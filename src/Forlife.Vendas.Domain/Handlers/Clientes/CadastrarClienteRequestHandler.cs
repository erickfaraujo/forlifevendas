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
    private readonly IClienteRepository _clienteRepository;
    private readonly ILogger<CadastrarClienteRequestHandler> _logger;

    public CadastrarClienteRequestHandler(IClienteRepository clienteRepository, ILogger<CadastrarClienteRequestHandler> logger)
    {
        _clienteRepository = clienteRepository;
        _logger = logger;
    }
    public async Task<Result<CadastrarClienteResponse>> Handle(CadastrarClienteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando cadastro de cliente");

        var cliente = new Cliente()
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Contato = request.Contato,
            DataNascimento = request.DataNascimento
        };

        var resultInsert = await _clienteRepository.CreateAsync(cliente);

        return resultInsert
            ? new CadastrarClienteResponse(cliente.Id)
            : new CadastrarClienteException();
    }
}