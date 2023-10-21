using Forlife.Vendas.Domain.Requests.Pedidos;
using System.Text.Json.Serialization;

namespace Forlife.Vendas.Domain.Models;

public class Pedido
{
    [JsonPropertyName("pk")]
    public string Pk { get; init; } = default!;

    [JsonPropertyName("sk")]
    public string Sk => string.Concat("PEDIDO#", IdPedido.ToString());

    [JsonPropertyName("idpedido")]
    public Guid IdPedido { get; init; } = default!;

    [JsonPropertyName("datapedido")]
    public string DataPedido { get; init; } = default!;

    [JsonPropertyName("valor")]
    public decimal Valor { get; init; }

    [JsonPropertyName("itens")]
    public IEnumerable<Item> Itens { get; set; } = default!;

    [JsonPropertyName("pagamentos")]
    public List<Pagamento> Pagamentos { get; set; } = default!;

    [JsonPropertyName("totalpagamento")]
    public decimal TotalPagamento { get; set; }

    [JsonPropertyName("observacoes")]
    public string Observacoes { get; set; } = default!;

    [JsonPropertyName("status")]
    public string Status { get; set; } = default!;
}

public record Pagamento(DateTime Data, decimal Valor);
