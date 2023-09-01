using Jwu.Constants;
using Jwu.Exceptions;

namespace Jwu.Extensions;

public static class Conversions
{
    public static string ToString(this KeyUse keyUse)
    {
        return keyUse switch
        {
            KeyUse.Encyption => "enc",
            KeyUse.Signature => "sig",
            _ => throw new ShouldNotHappenException()
        };
    }

    public static string ToString(this KeyType keyAlg)
    {
        return keyAlg switch
        {
            KeyType.EllipticCurve => "EC",
            KeyType.Rsa => "RSA",
            _ => throw new ShouldNotHappenException()
        };
    }

    public static int ToInt(this KeySize keySize)
    {
        return keySize switch
        {
            KeySize.Bits2048 => 2048,
            KeySize.Bits4096 => 4096,
            _ => throw new ShouldNotHappenException()
        };
    }

}
