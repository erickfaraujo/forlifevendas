using Forlife.Vendas.Domain.Responses.Clientes;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Clientes;

public record CadastrarClienteRequest(string Nome, string Telefone, string? Email, string? Observacao, DateTime? DataNascimento, Guid IdLocalVenda) : IRequest<Result<CadastrarClienteResponse>>;
