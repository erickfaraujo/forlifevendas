using Forlife.Vendas.Domain.Enums;
using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos;

public class CriarPedidoRequestHandler : IRequestHandler<CriarPedidoRequest, Result<CriarPedidoResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;

    public CriarPedidoRequestHandler(IForlifeVendasRepository forlifeVendasRepository)
        => _forlifeVendasRepository = forlifeVendasRepository;

    public async Task<Result<CriarPedidoResponse>> Handle(CriarPedidoRequest request, CancellationToken cancellationToken)
    {
        var cliente = await _forlifeVendasRepository.GetAsync<Cliente>(request.IdCliente, "PERFIL");
        if (cliente is null)
            return new ClienteNaoLocalizadoException();

        var pedido = new Pedido()
        {
            Pk = cliente.Pk,
            IdPedido = Guid.NewGuid(),
            IdLocal = cliente.IdLocal,
            DataPedido = request.DataPedido.ToString("yyyy-MM-dd"),
            Valor = request.ValorTotal,
            Itens = [], // não implementado, qndo o front estiver preparado, pegar valor do request
            Pagamentos = [],
            TotalPagamento = request.ValorPago,
            Observacoes = request.Observacoes,
            CodigosProdutos = request.CodProdutos,
            Status = request.ValorPago < request.ValorTotal ? Enum.GetName(StatusPagamento.PENDENTE)! : Enum.GetName(StatusPagamento.PAGO)!,
            InfosAdicionais = new (cliente.Nome, cliente.NomeLocal)
        };

        if (request.ValorPago > 0)
            pedido.Pagamentos.Add(new Pagamento(DateTime.Now, request.ValorPago));

        var result = await _forlifeVendasRepository.CreateAsync(pedido);

        return result
            ? new CriarPedidoResponse(pedido.Pk)
            : new CriarPedidoException();
    }
}
