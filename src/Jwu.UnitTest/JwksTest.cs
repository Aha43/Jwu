using Jwu.Methods;

namespace Jwu.UnitTest
{
    public class JwksTest
    {
        [Fact]
        public void CreatingSingleDefaultJwkShouldWork()
        {
            var (priv, pub) = Jwk.CreateKeys();

            Assert.Single(priv);
            Assert.Single(pub);
        }

    }

}
