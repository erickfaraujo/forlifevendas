﻿using Forlife.Vendas.Domain.Models;

namespace Forlife.Vendas.Domain.Responses.Clientes;

public record ConsultarClientesPorLocalResponse(IEnumerable<Cliente> Clientes);

