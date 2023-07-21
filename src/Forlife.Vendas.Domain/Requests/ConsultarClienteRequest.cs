using Forlife.Vendas.Domain.Responses;
using MediatR;

namespace Forlife.Vendas.Domain.Requests;

public record ConsultarClienteRequest(string Id) : IRequest<ConsultarClienteResponse>;

