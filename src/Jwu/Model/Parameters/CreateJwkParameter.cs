using Jwu.Constants;

namespace Jwu.Model.Parameters;

/// <summary>
/// Specification on how Json Web Keys are generated.
/// </summary>
internal class CreateJwkParameter
{
    /// <summary>Size of keys.</summary>
    public KeySize KeySize { get; set; }

    /// <summary>Algorithm the keys are used for. Default is "RS512".</summary>
    public string Alg { get; set; } = "RS512";

    /// <summary>Use of keys. Default is "sig".</summary>
    public KeyUse Use { get; set; } = KeyUse.Signature;

    /// <summary>Number of keys to generate. Default is 1.</summary>
    public int Count { get; set; } = 1;

    /// <summary>Output private keys in a json array even if generating only one key. No effect if Count > 1 (then output is always array). Default is true.</summary>
    public bool ForceArrayPrivate { get; set; } = true;

    /// <summary>Output public keys in a json array even if generating only one key. No effect if Count > 1 (then output is always array). Default is true.</summary>
    public bool ForceArrayPublic { get; set; } = true;

    /// <summary>Include Json Web Token (JWT) type claim "issued at" (iat). Default is false. If true the value will be UTC time when key created in UNIX time (seconds).</summary>
    public bool IncludeIat { get; set; }

    public int Exp { get; set; } = 1;

    public KeyTimeUnit KeyExpTimeUnit { get; set; }

    public bool IgnoreExpInPublic { get; set; }
    public bool IgnoreExpInPrivate { get; set; } = true;
}
