using System.Text.Json.Serialization;

namespace Forlife.Vendas.Domain.Models;

public class LocalVenda
{
    [JsonPropertyName("idlocal")]
    public Guid IdLocal { get; init; } = default!;

    [JsonPropertyName("descricao")]
    public string Descricao { get; init; } = default!;

    [JsonPropertyName("endereco")]
    public string Endereco { get; init; } = default!;

    [JsonPropertyName("referencia")]
    public string Referencia { get; init; } = default!;
}
