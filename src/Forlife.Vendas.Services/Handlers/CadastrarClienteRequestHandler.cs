using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Forlife.Vendas.Services.Handlers;

public class CadastrarClienteRequestHandler : IRequestHandler<CadastrarClienteRequest, CadastrarClienteResponse>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ILogger<CadastrarClienteRequestHandler> _logger;

    public CadastrarClienteRequestHandler(IClienteRepository clienteRepository, ILogger<CadastrarClienteRequestHandler> logger)
    {
        _clienteRepository = clienteRepository;
        _logger = logger;
    }
    public async Task<CadastrarClienteResponse> Handle(CadastrarClienteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando cadastro de cliente");

        var cliente = new Cliente()
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Contato = request.Contato,
            DataNascimento = request.DataNascimento
        };

        await _clienteRepository.CreateAsync(cliente);

        var response = new CadastrarClienteResponse(cliente.Id);

        return response;
    }
}