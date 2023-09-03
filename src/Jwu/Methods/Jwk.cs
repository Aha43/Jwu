using Jwu.Constants;
using Jwu.Model;
using Jwu.Model.Parameters;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jwu.Methods;

public static class Jwk
{
    public static (JsonWebKey[] priv, JsonWebKey[] pub) CreateKeys(CreateJwkParameter param)
    {
        throw new NotImplementedException();
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
