using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Jwu.XunitAssertions
{
    public static class JwuAsserions
    {
        public static void IsPair(this JsonWebKey priv, JsonWebKey pub)
        {
            Assert.False(priv == pub);
            Assert.True(priv.HasPrivateKey);
            Assert.False(pub.HasPrivateKey);
            Assert.True(priv.KeyId == pub.KeyId);
        }

    }

}