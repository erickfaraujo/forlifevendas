using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Clientes;

public record GetClientesRequest(string Nome, string Telefone, string IdLocal) : IRequest<Result<GetClientesResponse>>;

