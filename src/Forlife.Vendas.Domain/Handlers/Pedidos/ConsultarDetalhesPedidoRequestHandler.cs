using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos;

public class ConsultarDetalhesPedidoRequestHandler : IRequestHandler<ConsultarDetalhesPedidoRequest, Result<ConsultarDetalhesPedidoResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;

    public ConsultarDetalhesPedidoRequestHandler(IForlifeVendasRepository forlifeVendasRepository)
        => _forlifeVendasRepository = forlifeVendasRepository;

    public async Task<Result<ConsultarDetalhesPedidoResponse>> Handle(ConsultarDetalhesPedidoRequest request, CancellationToken cancellationToken)
    {
        var pedido = await _forlifeVendasRepository.GetPedidoByIdAsync(request.IdPedido);

        if (pedido is null) return new PedidoNaoLocalizadoException();

        var cliente = await _forlifeVendasRepository.GetAsync<Cliente>(pedido.Pk, "PERFIL");

        pedido.InfosAdicionais = new(cliente!.Nome, string.Empty);

        return pedido!.Pk is null
            ? new PedidoNaoLocalizadoException()
            : new ConsultarDetalhesPedidoResponse(pedido);
    }
}
