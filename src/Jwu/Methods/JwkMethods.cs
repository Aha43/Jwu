using Jwu.Constants;
using Jwu.Exceptions;
using Jwu.Extensions;
using Jwu.Model;
using Jwu.Model.Parameters;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jwu.Methods;

public static class JwkMethods
{
    private static void CreateRsaKeys(CreateJwkParameter param, JsonWebKey[] privs, JsonWebKey[] pubs, int n)
    {
        for (int i = 0; i < n; i++)
        {
            using RSA rsa = RSA.Create(param.KeySize.ToInt());

            var privKey = new RsaSecurityKey(rsa.ExportParameters(true));
            var pubKey = new RsaSecurityKey(rsa.ExportParameters(false));

            privs[i] = JsonWebKeyConverter.ConvertFromRSASecurityKey(privKey);
            pubs[i] = JsonWebKeyConverter.ConvertFromRSASecurityKey(pubKey);   
        }

        CommonPostCreation(privs, pubs, param);
    }

    private static void CreateEllipticKeys(CreateJwkParameter param, JsonWebKey[] privs, JsonWebKey[] pubs)
    {
        throw new NotImplementedException();
    }

    private static void CommonPostCreation(JsonWebKey[] privs, JsonWebKey[] pubs, CreateJwkParameter param)
    {
        int n = privs.Length;
        for (int i = 0; i < n; i++) 
        {
            if (param.IncludeKid || param.Jwks != KeyPart.None)
            {
                var kid = Guid.NewGuid().ToString();
                privs[i].KeyId = kid;
                pubs[i].KeyId = kid;
            }
        }
    }

    public static (JsonWebKey[] priv, JsonWebKey[] pub) CreateKeys(CreateJwkParameter? param = null, int n = 1)
    {
        if (n < 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(n)} < 0 : {n}");
        }

        param ??= new();

        var privs = new JsonWebKey[n];
        var pubs = new JsonWebKey[n];

        switch (param.KeyType)
        {
            case KeyType.Rsa: CreateRsaKeys(param, privs, pubs, n); break;
            case KeyType.EllipticCurve: CreateEllipticKeys(param, privs, pubs); break;
            default: throw new ShouldNotHappenException();
        }

        return (privs, pubs);
    }

    public static (Jwk[] priv, Jwk[] pub) CreateSerializableKeys(CreateJwkParameter? param = null, int n = 1)
    {
        var (priv, pub) = CreateKeys(param, n);
        var privSer = priv.Select(e => Jwk.FromJsonWebKey(e)).ToArray();
        var pubSer = pub.Select(e => Jwk.FromJsonWebKey(e)).ToArray();
        return (privSer, pubSer);
    }
   
    public static (string privJson, string pubJson) CreateKeysJson(CreateJwkParameter? param = null, int n = 1)
    {
        param ??= new();

        var (priv, pub) = CreateSerializableKeys(param, n);

        object pubSer = pub;
        if (MakePublicSingle(param, n)) pubSer = pub[0];

        object privSer = priv;
        if (MakePrivateSingle(param, n)) privSer = priv[0];

        if (param.Jwks == KeyPart.Public || param.Jwks == KeyPart.Both) pubSer = new Jwks { Keys = pub };

        if (param.Jwks == KeyPart.Private || param.Jwks == KeyPart.Both) privSer = new Jwks { Keys = priv };

        var opriv = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = param.PrettyJson == KeyPart.Private || param.PrettyJson == KeyPart.Both
        };
        var opub = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = param.PrettyJson == KeyPart.Public || param.PrettyJson == KeyPart.Both
        };

        var prj = JsonSerializer.Serialize(privSer, opriv);
        var puj = JsonSerializer.Serialize(pubSer, opub);

        return (prj, puj);
    }

    private static bool MakePrivateSingle(CreateJwkParameter param, int n)
    {
        if (n > 1) return false;
        return param.ForceSingle == KeyPart.Private || param.ForceSingle == KeyPart.Both;
    }

    private static bool MakePublicSingle(CreateJwkParameter param, int n)
    {
        if (n > 1) return false;
        return param.ForceSingle == KeyPart.Public || param.ForceSingle == KeyPart.Both;
    }

}
