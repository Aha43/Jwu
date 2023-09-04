using Jwu.Constants;
using Jwu.Exceptions;
using Jwu.Extensions;
using Jwu.Model;
using Jwu.Model.Parameters;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Jwu.Methods;

public static class Jwk
{
    private static void CreateRsaKeys(CreateJwkParameter param, JsonWebKey[] privs, JsonWebKey[] pubs)
    {
        for (int i = 0; i < param.Count; i++)
        {
            using RSA rsa = RSA.Create(param.KeySize.ToInt());

            var privKey = new RsaSecurityKey(rsa.ExportParameters(true));
            var pubKey = new RsaSecurityKey(rsa.ExportParameters(false));

            privs[i] = JsonWebKeyConverter.ConvertFromRSASecurityKey(privKey);
            pubs[i] = JsonWebKeyConverter.ConvertFromRSASecurityKey(pubKey);
        }
    }

    private static void CreateEllipticKeys(CreateJwkParameter param, JsonWebKey[] privs, JsonWebKey[] pubs)
    {
        throw new NotImplementedException();
    }

    public static (JsonWebKey[] priv, JsonWebKey[] pub) CreateKeys(CreateJwkParameter param)
    {
        var privs = new JsonWebKey[param.Count];
        var pubs = new JsonWebKey[param.Count];

        switch (param.KeyType)
        {
            case KeyType.Rsa: CreateRsaKeys(param, privs, pubs); break;
            case KeyType.EllipticCurve: CreateEllipticKeys(param, privs, pubs); break;
            default: throw new ShouldNotHappenException();
        }

        return (privs, pubs);
    }

    public static (JsonSerializableJwk[] priv, JsonSerializableJwk[] pub) CreateSerializableKeys(CreateJwkParameter param)
    {
        var (priv, pub) = CreateKeys(param);
        var privSer = priv.Select(e => new JsonSerializableJwk(e)).ToArray();
        var pubSer = pub.Select(e => new JsonSerializableJwk(e)).ToArray();
        return (privSer, pubSer);
    }
   
    public static (string privJson, string pubJson) CreateKeysJson(CreateJwkParameter param)
    {
        var (priv, pub) = CreateSerializableKeys(param);

        var privSb = new StringBuilder();
        var pubSb = new StringBuilder();

        var privArray = MakePrivateArray(param);
        var pubArray = MakePublicArray(param);

        if (privArray) privSb.AppendLine("[");
        if (pubArray) pubSb.AppendLine("[");

        for (int i = 0; i < param.Count; i++)
        {
            if (i > 0)
            {
                privSb.AppendLine(",");
                pubSb.AppendLine(",");
            }

            privSb.Append(priv[i].ToJson());
            pubSb.Append(pub[i].ToJson());
        }

        if (privArray) privSb.AppendLine("]");
        if (pubArray) pubSb.AppendLine("]");

        return (privSb.ToString(), pubSb.ToString());
    }

    private static bool MakePrivateArray(CreateJwkParameter param) =>
        param.Count > 1 || param.ForceArray == KeyPart.Private || param.ForceArray == KeyPart.Both;

    private static bool MakePublicArray(CreateJwkParameter param) =>
        param.Count > 1 || param.ForceArray == KeyPart.Public || param.ForceArray == KeyPart.Both;

}
