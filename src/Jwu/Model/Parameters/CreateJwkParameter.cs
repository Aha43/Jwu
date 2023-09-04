using Jwu.Constants;

namespace Jwu.Model.Parameters;

/// <summary>
/// Specification on how Json Web Keys are generated.
/// </summary>
public sealed class CreateJwkParameter
{
    /// <summary>Key type to create. Default is RSA.</summary>
    public KeyType KeyType { get; set; }

    /// <summary>Size of keys.</summary>
    public KeySize KeySize { get; init; } = KeySize.Bits2048;

    /// <summary>Algorithm the keys are used for. Default is "RS512".</summary>
    public string Alg { get; init; } = "RS512";

    /// <summary>Use of keys. Default is "sig".</summary>
    public KeyUse Use { get; init; } = KeyUse.Signature;

    /// <summary>Number of keys to generate. Default is 1.</summary>
    public uint Count { get; init; } = 1;

    /// <summary>Output keys in a json array even if generating only one key for the specified parts. No effect if Count > 1 (then output is always array). Default is true.</summary>
    public KeyPart ForceArray { get; init; }

    /// <summary>Include Json Web Token (JWT) type claim "issued at" (iat). Default is false. If true the value will be UTC time when key created in UNIX time (seconds).</summary>
    public KeyPart IncludeIat { get; init; }

    /// <summary>Specifies if and where exp (expires) property is to be included in json produced.</summary>
    public JwtTime Exp { get; init; }

    /// <summary>Specifies if and where nbf (not before) property is to be included in json produced.</summary>
    public JwtTime Nbf { get; init; }

}

public static class Extensions
{

    public static bool NeedTime(this CreateJwkParameter param) => 
        param.IncludeIat != default || param.Exp.RepresentKeyTime() || param.Nbf.RepresentKeyTime();
}
