using System.Text.Json.Serialization;

namespace Forlife.Vendas.Domain.Models;

public class LocalVenda
{
    [JsonPropertyName("pk")]
    public Guid Pk => Id;

    [JsonPropertyName("sk")]
    public Guid Sk => Id;

    public Guid Id { get; init; } = default!;

    public string Descricao { get; init; } = default!;

    public string Endereco { get; init; } = default!;

    public string? Referencia { get; init; }
}
