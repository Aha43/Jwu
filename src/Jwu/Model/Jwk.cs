using Jwu.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jwu.Model;

/// <summary>
/// Json serializable representation of a Json Web Key (JWK)
/// </summary>
public sealed class Jwk
{
    /// <summary>'alg' (KeyType)</summary>
    [JsonPropertyName("alg")]
    public string? Alg { get; set; }

    /// <summary>'crv' (ECC - Curve)</summary>
    [JsonPropertyName("crv")]
    public string? Crv { get; set; }

    /// <summary>'d' (ECC - Private Key OR RSA - Private Exponent)</summary>
    [JsonPropertyName("d")]
    public string? D { get; set; }

    /// <summary>'dp' (RSA - First Factor CRT Exponent)</summary>
    [JsonPropertyName("dp")]
    public string? DP { get; set; }

    /// <summary>'dq' (RSA - Second Factor CRT Exponent)</summary>
    [JsonPropertyName("dq")]
    public string? DQ { get; set; }

    /// <summary>'e' (RSA - Exponent)</summary>
    [JsonPropertyName("e")]
    public string? E { get; set; }

    /// <summary>'k' (Symmetric - Key Value)</summary>
    [JsonPropertyName("k")]
    public string? K { get; set; }

    /// <summary>'key_ops' (Key Operations)</summary>
    [JsonPropertyName("key_ops")]
    public IList<string>? KeyOps { get; set; }

    /// <summary>'kid' (Key ID)</summary>
    [JsonPropertyName("kid")]
    public required string Kid { get; set; }

    /// <summary>'kty' (Key Type)</summary>
    [JsonPropertyName("kty")]
    public string? Kty { get; set; }

    /// <summary>'n' (RSA - Modulus)</summary>
    [JsonPropertyName("n")]
    public string? N { get; set; }

    /// <summary>'oth' (RSA - Other Primes Info)</summary>
    [JsonPropertyName("oth")]
    public IList<string>? Oth { get; set; }

    /// <summary>'p' (RSA - First Prime Factor)</summary>
    [JsonPropertyName("p")]
    public string? P { get; set; }

    /// <summary>'q' (RSA - Second Prime Factor)</summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>'qi' (RSA - First CRT Coefficient)</summary>
    [JsonPropertyName("qi")]
    public string? QI { get; set; }

    /// <summary>'use' (Public Key Use)</summary>
    [JsonPropertyName("use")]
    public string? Use { get; set; }

    /// <summary>'x' (ECC - X Coordinate)</summary>
    [JsonPropertyName("x")]
    public string? X { get; set; }

    /// <summary>'x5c' collection (X.509 Certificate Chain)</summary>
    [JsonPropertyName("x5c")]
    public IList<string>? X5c { get; set; }

    /// <summary>'x5t' (X.509 Certificate SHA-1 thumbprint)</summary>
    [JsonPropertyName("x5t")]
    public string? X5t { get; set; }

    /// <summary>'x5t#S256' (X.509 Certificate SHA-256 thumbprint)</summary>
    [JsonPropertyName("x5t#S256")]
    public string? X5tS256 { get; set; }

    /// <summary>'x5u' (X.509 URL)</summary>
    [JsonPropertyName("x5u")]
    public string? X5u { get; set; }

    /// <summary>'y' (ECC - Y Coordinate)</summary>
    [JsonPropertyName("y")]
    public string? Y { get; set; }

    //
    // Extra "JWT" properties, not specified by the RFC but is used by application. Not part of JsonWebKey type.
    //

    /// <summary>Expires</summary>
    [JsonPropertyName("exp")]
    public long? Exp { get; set; }

    /// <summary>Issues at</summary>
    [JsonPropertyName("iat")]
    public long? Iat { get; set; }

    /// <summary>Not before</summary>
    [JsonPropertyName("nbf")]
    public long? Nbf { get; set; }

    public override string ToString() => ToJson();

    /// <returns>Json</returns>
    public string ToJson(bool pretty = false)
    {
        var retVal = JsonSerializer.Serialize(this, new JsonSerializerOptions 
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = pretty
        });
        return retVal;
    }

    public static Jwk FromJson(string json)
    {
        var retVal = JsonSerializer.Deserialize<Jwk>(json);
        return retVal ?? throw new ShouldNotHappenException();
    }

    /// <returns>JsonWebKey</returns>
    public JsonWebKey ToJsonWebKey()
    {
        var json = ToJson();
        var retVal = new JsonWebKey(json);
        return retVal;
    }

    public static Jwk FromJsonWebKey(JsonWebKey jwk)
    {
        return new Jwk
        {
            Alg = jwk.Alg,
            Crv = jwk.Crv,
            D = jwk.D,
            DP = jwk.DP,
            DQ = jwk.DQ,
            E = jwk.E,
            K = jwk.K,
            KeyOps = jwk.KeyOps?.Count > 0 ? jwk.KeyOps : null,
            Kid = jwk.KeyId,
            Kty = jwk.Kty,
            N = jwk.N,
            Oth = jwk.Oth,
            P = jwk.P,
            Q = jwk.Q,
            QI = jwk.QI,
            Use = jwk.Use,
            X = jwk.X,
            Y = jwk.Y,
            X5c = jwk.X5c?.Count > 0 ? jwk.X5c : null,
            X5t = jwk.X5t,
            X5u = jwk.X5u,
            X5tS256 = jwk.X5tS256
        };
    }

}
