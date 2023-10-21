using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos;

public class ConsultarPedidosPorDataRequestHandler : IRequestHandler<ConsultarPedidosPorDataRequest, Result<ConsultarPedidosPorDataResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;

    public ConsultarPedidosPorDataRequestHandler(IForlifeVendasRepository forlifeVendasRepository)
        => _forlifeVendasRepository = forlifeVendasRepository;

    public async Task<Result<ConsultarPedidosPorDataResponse>> Handle(ConsultarPedidosPorDataRequest request, CancellationToken cancellationToken)
    {
        var pedidos = await _forlifeVendasRepository.GetPedidosPorDataAsync(request.DataInicio, request.DataFim);

        if (!pedidos!.Any())
            return new PedidoNaoLocalizadoException();

        return new ConsultarPedidosPorDataResponse(pedidos!);
    }
}
