﻿using Forlife.Vendas.Domain.Responses.LocaisVenda;
using MediatR;
using OperationResult;

namespace Forlife.Vendas.Domain.Requests.LocaisVenda;

public record GetLocaisRequest(string Descricao, string Endereco) : IRequest<Result<GetLocaisResponse>>;

