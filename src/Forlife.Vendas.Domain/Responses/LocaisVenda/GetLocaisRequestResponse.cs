﻿using Forlife.Vendas.Domain.Models;

namespace Forlife.Vendas.Domain.Responses.LocaisVenda;

public record GetLocaisResponse(IEnumerable<LocalVenda> LocaisVenda);

