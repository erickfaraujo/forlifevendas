using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos;

public class ConsultarPedidosRequestHandler : IRequestHandler<ConsultarPedidosRequest, Result<ConsultarPedidosResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILocalVendaRepository _localVendaRepository;
    private readonly ILogger<ConsultarPedidosRequestHandler> _logger;

    public ConsultarPedidosRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILocalVendaRepository localVendaRepository, ILogger<ConsultarPedidosRequestHandler> logger)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _localVendaRepository = localVendaRepository;
        _logger = logger;
    }

    public async Task<Result<ConsultarPedidosResponse>> Handle(ConsultarPedidosRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("iniciando a consulta de pedidos");
        var pedidos = await _forlifeVendasRepository.GetPedidosAsync(request);

        if (pedidos!.Count == 0) return new PedidoNaoLocalizadoException();

        _logger.LogInformation("Encontrado {pedidos} pedidos", pedidos.Count);

        //foreach (var pedido in pedidos!)
        //{
        //    //var cliente = await _forlifeVendasRepository.GetAsync<Cliente>(pedido.Pk, "PERFIL");
        //    //var local = await _localVendaRepository.GetAsync(Guid.Parse(pedido.IdLocal));
        //    //pedido.InfosAdicionais = new(cliente!.Nome, local!.Descricao, cliente.Pk);
        //    pedido.InfosAdicionais = new(cliente!.Nome, "---");
        //}

        _logger.LogInformation("Retornando pedidos");

        return new ConsultarPedidosResponse(pedidos!);
    }

}
