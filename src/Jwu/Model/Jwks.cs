using System.Text.Json.Serialization;

namespace Jwu.Model;

public sealed class Jwks
{
    [JsonPropertyName("keys")]
    public IEnumerable<Jwk> Keys { get; set; } = Enumerable.Empty<Jwk>();
}
