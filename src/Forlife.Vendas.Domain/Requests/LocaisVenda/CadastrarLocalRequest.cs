using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.LocaisVenda;

public record CadastrarLocalRequest(string Descricao, string Endereco, string? Referencia) : IRequest<Result<CadastrarLocalResponse>>;
