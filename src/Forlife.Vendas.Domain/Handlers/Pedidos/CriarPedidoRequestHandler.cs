using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using Newtonsoft.Json;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos
{
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
                DataPedido = DateTime.Now.ToString("yyyy-MM-dd"),
                Valor = request.ValorTotal,
                Itens = request.Itens,
                TotalPagamento = request.ValorPago,
                Status = request.ValorPago < request.ValorTotal ? "pendente" : "pago"
            };

            var pagamentos = new Dictionary<string, decimal>() { { DateTime.Now.ToShortDateString(), request.ValorPago } };

            pedido.Pagamentos = JsonConvert.SerializeObject(pagamentos);

            var result = await _forlifeVendasRepository.CreateAsync(pedido);

            return result
                ? new CriarPedidoResponse(pedido.Pk)
                : new CriarPedidoException();
        }
    }
}
