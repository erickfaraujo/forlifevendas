using MediatR;

namespace Forlife.Vendas.Domain.Requests;

public record CadastrarClienteRequest(string Nome, string Contato, DateTime DataNascimento) : IRequest<CadastrarClienteResponse>;
