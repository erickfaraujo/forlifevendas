using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Pedidos;
using Forlife.Vendas.Domain.Responses.Pedidos;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Pedidos
{
    public class ConsultarPedidosClienteRequestHandler : IRequestHandler<ConsultarPedidosClienteRequest, Result<ConsultarPedidosClienteResponse>>
    {
        private readonly IForlifeVendasRepository _forlifeVendasRepository;

        public ConsultarPedidosClienteRequestHandler(IForlifeVendasRepository forlifeVendasRepository)
            => _forlifeVendasRepository = forlifeVendasRepository;

        public async Task<Result<ConsultarPedidosClienteResponse>> Handle(ConsultarPedidosClienteRequest request, CancellationToken cancellationToken)
        {
            var pedidos = await _forlifeVendasRepository.GetPedidosClienteAsync(request.IdCliente);

            if (!request.Pagos)
            {
                for (var i = pedidos!.Count - 1; i >= 0; i--)
                {
                    if (pedidos[i].Status is "pago")
                        pedidos.RemoveAt(i);
                }
            }

            if (!pedidos!.Any())
                return new PedidoNaoLocalizadoException();

            return new ConsultarPedidosClienteResponse(pedidos!);
        }
    }
}
