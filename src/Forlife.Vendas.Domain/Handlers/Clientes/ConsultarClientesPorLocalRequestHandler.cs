using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class ConsultarClientesPorLocalRequestHandler : IRequestHandler<ConsultarClientesPorLocalRequest, Result<ConsultarClientesPorLocalResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public ConsultarClientesPorLocalRequestHandler(IClienteRepository clienteRepository)
        => _clienteRepository = clienteRepository;

    public async Task<Result<ConsultarClientesPorLocalResponse>> Handle(ConsultarClientesPorLocalRequest request, CancellationToken cancellationToken)
    {
        var clientes = await _clienteRepository.GetByIdLocalAsync(Guid.Parse(request.IdLocal));

        return clientes is null
            ? new ClienteNaoLocalizadoException()
            : new ConsultarClientesPorLocalResponse(clientes);
    }
}