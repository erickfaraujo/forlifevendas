using System.Text.Json.Serialization;

namespace Forlife.Vendas.Domain.Models;

public class Cliente
{
    [JsonPropertyName("pk")]
    public Guid Pk => Id;

    [JsonPropertyName("sk")]
    public Guid Sk => Id;

    public Guid Id { get; init; } = default!;

    public string Nome { get; init; } = default!;

    public string Contato { get; init; } = default!;

    public DateTime DataNascimento { get; init; }

    public Guid LocalVenda { get; set; } = default!;
}
