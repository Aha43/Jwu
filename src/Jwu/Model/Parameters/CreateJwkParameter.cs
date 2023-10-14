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

    /// <summary>Output key not in a json when generating only one key for the specified parts. No effect if Count > 1 (then output is always array). Default is none.</summary>
    public KeyPart ForceSingle { get; init; }

    /// <summary>Which key parts to output in the jwks (json web key set). Default is none</summary>
    public KeyPart Jwks { get; init; }

    /// <summary>If true generate kid for keys, default is false.</summary>
    public bool IncludeKid { get; init; }

    /// <summary>Include Json Web Token (JWT) type claim "issued at" (iat). Default is false. If true the value will be UTC time when key created in UNIX time (seconds).</summary>
    public KeyPart IncludeIat { get; init; }

    /// <summary>Specifies if and where exp (expires) property is to be included in json produced.</summary>
    public JwtTime Exp { get; init; }

    /// <summary>Specifies if and where nbf (not before) property is to be included in json produced.</summary>
    public JwtTime Nbf { get; init; }

    /// <summary>Specify parts of key should be pretty json (indented), default is none.</summary>
    public KeyPart PrettyJson { get; init; }

}

public static class Extensions
{

    public static bool NeedTime(this CreateJwkParameter param) => 
        param.IncludeIat != default || param.Exp.RepresentKeyTime() || param.Nbf.RepresentKeyTime();
}
