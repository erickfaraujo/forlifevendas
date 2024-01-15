using Forlife.Vendas.Domain.Responses.Resumo;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Resumo;

public record GetResumoRequest() : IRequest<Result<GetResumoResponse>>;
