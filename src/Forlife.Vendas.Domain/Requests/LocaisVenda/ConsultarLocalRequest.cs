using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.LocaisVenda;

public record ConsultarLocalRequest(string Id) : IRequest<Result<ConsultarLocalResponse>>;

