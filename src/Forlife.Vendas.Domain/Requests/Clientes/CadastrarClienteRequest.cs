using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Clientes;

public record CadastrarClienteRequest(string Nome, string Contato, DateTime DataNascimento, Guid IdLocalVenda) : IRequest<Result<CadastrarClienteResponse>>;
