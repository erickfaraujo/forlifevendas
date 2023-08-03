using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class ConsultarClienteRequestHandler : IRequestHandler<ConsultarClienteRequest, Result<ConsultarClienteResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;

    public ConsultarClienteRequestHandler(IForlifeVendasRepository forlifeVendasRepository)
        => _forlifeVendasRepository = forlifeVendasRepository;

    public async Task<Result<ConsultarClienteResponse>> Handle(ConsultarClienteRequest request, CancellationToken cancellationToken)
    {
        var cliente = await _forlifeVendasRepository.GetAsync<Cliente>(request.Id, "PERFIL");

        return cliente is null
            ? new ClienteNaoLocalizadoException()
            : new ConsultarClienteResponse(cliente);
    }
}