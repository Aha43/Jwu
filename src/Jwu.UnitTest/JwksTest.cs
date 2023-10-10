using Jwu.Methods;
using Jwu.XunitAssertions;

namespace Jwu.UnitTest
{
    public class JwksTest
    {
        [Fact]
        public void CreatingSingleDefaultJwkShouldWork()
        {
            var (priv, pub) = JwkMethods.CreateKeys();

            Assert.Single(priv);
            Assert.Single(pub);

            var privKey = priv[0];
            Assert.NotNull(privKey);

            var pubKey = pub[0];
            Assert.NotNull(pubKey);

            JwuAssertions.IsPair(privKey, pubKey);
        }

    }

}
