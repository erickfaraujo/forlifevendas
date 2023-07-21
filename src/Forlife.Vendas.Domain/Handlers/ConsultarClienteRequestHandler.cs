using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests;
using Forlife.Vendas.Domain.Responses;
using MediatR;

namespace Forlife.Vendas.Domain.Handlers;

public class ConsultarClienteRequestHandler : IRequestHandler<ConsultarClienteRequest, ConsultarClienteResponse>
{
    private readonly IClienteRepository _clienteRepository;

    public ConsultarClienteRequestHandler(IClienteRepository clienteRepository)
        => _clienteRepository = clienteRepository;
    

    public async Task<ConsultarClienteResponse> Handle(ConsultarClienteRequest request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.GetAsync(Guid.Parse(request.Id));
        return cliente is null
            ? new ConsultarClienteResponse(null) 
            : new ConsultarClienteResponse(cliente);
    }
}


