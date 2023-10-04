﻿using Jwu.Constants;
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
    }

    private static void CreateEllipticKeys(CreateJwkParameter param, JsonWebKey[] privs, JsonWebKey[] pubs)
    {
        throw new NotImplementedException();
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

    public static (JsonSerializableJwk[] priv, JsonSerializableJwk[] pub) CreateSerializableKeys(CreateJwkParameter? param = null, int n = 1)
    {
        var (priv, pub) = CreateKeys(param, n);
        var privSer = priv.Select(e => JsonSerializableJwk.FromJsonWebKey(e)).ToArray();
        var pubSer = pub.Select(e => JsonSerializableJwk.FromJsonWebKey(e)).ToArray();
        return (privSer, pubSer);
    }
   
    public static (string privJson, string pubJson) CreateKeysJson(CreateJwkParameter? param = null, int n = 1)
    {
        param ??= new();

        var (priv, pub) = CreateSerializableKeys(param, n);

        var privSb = new StringBuilder();
        var pubSb = new StringBuilder();

        

        var privArray = MakePrivateArray(param, n);
        var pubArray = MakePublicArray(param, n);

        if (privArray) privSb.AppendLine("[");
        if (pubArray) pubSb.AppendLine("[");

        for (int i = 0; i < n; i++)
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

    private static bool MakePrivateArray(CreateJwkParameter param, int n) =>
        n > 1 || param.ForceArray == KeyPart.Private || param.ForceArray == KeyPart.Both;

    private static bool MakePublicArray(CreateJwkParameter param, int n) =>
        n > 1 || param.ForceArray == KeyPart.Public || param.ForceArray == KeyPart.Both;

}
