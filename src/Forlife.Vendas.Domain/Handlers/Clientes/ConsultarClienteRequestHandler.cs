using Forlife.Vendas.Domain.Exceptions;
using Forlife.Vendas.Domain.Models;
using Forlife.Vendas.Domain.Repositories;
using Forlife.Vendas.Domain.Requests.Clientes;
using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Handlers.Clientes;

public class ConsultarClienteRequestHandler : IRequestHandler<ConsultarClienteRequest, Result<ConsultarClienteResponse>>
{
    private readonly IForlifeVendasRepository _forlifeVendasRepository;
    private readonly ILocalVendaRepository _localVendaRepository;

    public ConsultarClienteRequestHandler(IForlifeVendasRepository forlifeVendasRepository, ILocalVendaRepository localVendaRepository)
    {
        _forlifeVendasRepository = forlifeVendasRepository;
        _localVendaRepository = localVendaRepository;
    }

    public async Task<Result<ConsultarClienteResponse>> Handle(ConsultarClienteRequest request, CancellationToken cancellationToken)
    {
        var cliente = await _forlifeVendasRepository.GetAsync<Cliente>(request.Id, "PERFIL")
            ?? throw new ClienteNaoLocalizadoException();

        var locais = await _localVendaRepository.GetAllAsync();

        var local = locais!.FirstOrDefault(local => local.IdLocal == Guid.Parse(cliente.IdLocal))
            ?? throw new LocalNaoLocalizadoException();

        cliente.NomeLocal = local!.Descricao;

        return new ConsultarClienteResponse(cliente);
    }
}