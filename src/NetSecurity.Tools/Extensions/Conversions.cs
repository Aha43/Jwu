using NetSecurity.Tools.Constants;
using NetSecurity.Tools.Exceptions;

namespace NetSecurity.Tools.Extensions;

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

    public static string ToString(this KeyAlg keyAlg) 
    {
        return keyAlg switch
        {
            KeyAlg.EllipticCurve => "EC",
            KeyAlg.Rsa => "RSA",
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
