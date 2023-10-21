using System.Text.Json.Serialization;

namespace Forlife.Vendas.Domain.Models;

public class Cliente
{
    [JsonPropertyName("pk")]
    public string Pk { get; init; } = default!;

    [JsonPropertyName("sk")]
    public string Sk => "PERFIL";

    [JsonPropertyName("nome")]
    public string Nome { get; set; } = default!;

    [JsonPropertyName("telefone")]
    public string Telefone { get; set; } = default!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    [JsonPropertyName("dtnascimento")]
    public string DtNascimento { get; set; } = default!;

    [JsonPropertyName("idlocal")]
    public string IdLocal { get; set; } = default!;

    public string NomeLocal { get; set; } = default!;
}
