using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class ConsultarClientesPorLocalRequestHandler : IRequestHandler<ConsultarClientesPorLocalRequest, Result<ConsultarClientesPorLocalResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;

    public ConsultarClientesPorLocalRequestHandler(IForlifeVendasRepository forlifeVendasRepository)
        => _forlifeVendasRepository = forlifeVendasRepository;

    public async Task<Result<ConsultarClientesPorLocalResponse>> Handle(ConsultarClientesPorLocalRequest request, CancellationToken cancellationToken)
    {
        var clientes = await _forlifeVendasRepository.GetClienteByIdLocalAsync(Guid.Parse(request.IdLocal));

        return clientes is null || !clientes.Any()
            ? new ClienteNaoLocalizadoException()
            : new ConsultarClientesPorLocalResponse(clientes);
    }
}