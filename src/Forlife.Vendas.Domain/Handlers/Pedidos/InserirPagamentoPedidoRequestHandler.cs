using Forlife.Vendas.Domain.Enums;
using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos;

public class InserirPagamentoPedidoRequestHandler : IRequestHandler<InserirPagamentoPedidoRequest, Result<InserirPagamentoPedidoResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILogger<InserirPagamentoPedidoRequestHandler> _logger;

    public InserirPagamentoPedidoRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILogger<InserirPagamentoPedidoRequestHandler> logger)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _logger = logger;
    }
    public async Task<Result<InserirPagamentoPedidoResponse>> Handle(InserirPagamentoPedidoRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inserindo pagamento de pedido");

        var pedido = await _forlifeVendasRepository.GetPedidoByIdAsync(request.IdPedido);

        if (pedido is null || pedido.Pk is null)
            return Result.Error<InserirPagamentoPedidoResponse>(new PedidoNaoLocalizadoException());

        var pgto = new Pagamento(request.Data, request.Valor);
        pedido.Pagamentos.Add(pgto);

        pedido.TotalPagamento = pedido.Pagamentos.Sum(x => x.Valor);

        if (pedido.TotalPagamento >= pedido.Valor)
            pedido.Status = Enum.GetName(StatusPagamento.PAGO)!;

        await _forlifeVendasRepository.CreateAsync(pedido);

        return new InserirPagamentoPedidoResponse(pedido);
    }
}
