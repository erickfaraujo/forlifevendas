using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class DeletarClienteRequestHandler : IRequestHandler<DeletarClienteRequest, Result<DeletarClienteResponse>>
{
    private readonly IClienteRepository _clienteRepository;

    public DeletarClienteRequestHandler(IClienteRepository clienteRepository)
        => _clienteRepository = clienteRepository;

    public async Task<Result<DeletarClienteResponse>> Handle(DeletarClienteRequest request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.DeleteAsync(Guid.Parse(request.Id));
        if (!cliente)
              return new DeletarClienteException();
        
        return new DeletarClienteResponse();
  
    }
}