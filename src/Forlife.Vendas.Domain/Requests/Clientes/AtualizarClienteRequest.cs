﻿using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.Clientes;

public record AtualizarClienteRequest(string Id, string Nome, string Telefone, string Email, Guid IdLocalVenda) : IRequest<Result<bool>>;
