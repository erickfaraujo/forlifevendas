using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos;

public class ConsultarPedidosRequestHandler : IRequestHandler<ConsultarPedidosRequest, Result<ConsultarPedidosResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILocalVendaRepository _localVendaRepository;

    public ConsultarPedidosRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILocalVendaRepository localVendaRepository)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _localVendaRepository = localVendaRepository;
    }

    public async Task<Result<ConsultarPedidosResponse>> Handle(ConsultarPedidosRequest request, CancellationToken cancellationToken)
    {
        var pedidos = await _forlifeVendasRepository.GetPedidosAsync(request);

        if (!pedidos!.Any()) return new PedidoNaoLocalizadoException();

        foreach (var pedido in pedidos!)
        {
            var cliente = await _forlifeVendasRepository.GetAsync<Cliente>(pedido.Pk, "PERFIL");
            var local = await _localVendaRepository.GetAsync(Guid.Parse(pedido.IdLocal));
            pedido.InfosAdicionais = new(cliente!.Nome, local!.Descricao);
        }

        return new ConsultarPedidosResponse(pedidos!);
    }

}
