using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos;

public class EditarPedidoRequestHandler : IRequestHandler<EditarPedidoRequest, Result<EditarPedidoResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILogger<InserirPagamentoPedidoRequestHandler> _logger;

    public EditarPedidoRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILogger<InserirPagamentoPedidoRequestHandler> logger)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _logger = logger;
    }
    public async Task<Result<EditarPedidoResponse>> Handle(EditarPedidoRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Editando pedido {idPedido}", request.IdPedido);

        var pedido = await _forlifeVendasRepository.GetPedidoByIdAsync(request.IdPedido);

        if (pedido is null || pedido.Pk is null)
            throw new PedidoNaoLocalizadoException();

        pedido.Valor = request.ValorTotal;
        pedido.Observacoes = request.Observacoes;
        pedido.CodigosProdutos = request.CodProdutos;

        await _forlifeVendasRepository.CreateAsync(pedido);

        return new EditarPedidoResponse(pedido);
    }
}
