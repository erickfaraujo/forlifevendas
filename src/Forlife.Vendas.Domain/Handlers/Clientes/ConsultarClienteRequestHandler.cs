using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class ConsultarClienteRequestHandler : IRequestHandler<ConsultarClienteRequest, Result<ConsultarClienteResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public ConsultarClienteRequestHandler(IClienteRepository clienteRepository)
        => _clienteRepository = clienteRepository;

    public async Task<Result<ConsultarClienteResponse>> Handle(ConsultarClienteRequest request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.GetAsync(Guid.Parse(request.Id));

        return cliente is null
            ? new ClienteNaoLocalizadoException()
            : new ConsultarClienteResponse(cliente);
    }
}