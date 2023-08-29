using System.Text.Json.Serialization;

namespace NetSecurity.Tools.Model
{
    public class SeriableJwk
    {
        /// <summary>'alg' (KeyType)</summary>
        [JsonPropertyName("alg")]
        public required string Alg { get; set; }

        /// <summary>'crv' (ECC - Curve)</summary>
        [JsonPropertyName("crv")]
        public string? Crv { get; set; }

        /// <summary>'d' (ECC - Private Key OR RSA - Private Exponent)</summary>
        [JsonPropertyName("d")]
        public string? D { get; set; }

        /// <summary>'dp' (RSA - First Factor CRT Exponent)</summary>
        [JsonPropertyName("dp")]
        public string? DP { get; set; }

        /// <summary>'dq' (RSA - Second Factor CRT Exponent)</summary>
        [JsonPropertyName("dq")]
        public string? DQ { get; set; }

        /// <summary>'e' (RSA - Exponent)</summary>
        [JsonPropertyName("e")]
        public string? E { get; set; }

        /// <summary>'k' (Symmetric - Key Value)</summary>
        [JsonPropertyName("k")]
        public string? K { get; set; }

        /// <summary>'key_ops' (Key Operations)</summary>
        [JsonPropertyName("key_ops")]
        public IList<string>? KeyOps { get; set; }

        /// <summary>'kid' (Key ID)</summary>
        [JsonPropertyName("kid")]
        public string? Kid { get; set; }

        /// <summary>'kty' (Key Type)</summary>
        [JsonPropertyName("kty")]
        public string? Kty { get; set; }

        /// <summary>'n' (RSA - Modulus)</summary>
        [JsonPropertyName("n")]
        public string? N { get; set; }

        /// <summary>'oth' (RSA - Other Primes Info)</summary>
        [JsonPropertyName("oth")]
        public IList<string> Oth { get; set; }

        /// <summary>'p' (RSA - First Prime Factor)</summary>
        [JsonPropertyName("p")]
        public string? P { get; set; }

        /// <summary>'q' (RSA - Second Prime Factor)</summary>
        [JsonPropertyName("q")]
        public string? Q { get; set; }

        /// <summary>'qi' (RSA - First CRT Coefficient)</summary>
        [JsonPropertyName("qi")]
        public string? QI { get; set; }

        /// <summary>'use' (Public Key Use)</summary>
        [JsonPropertyName("use")]
        public string? Use { get; set; }

        /// <summary>'x' (ECC - X Coordinate)</summary>
        [JsonPropertyName("x")]
        public string? X { get; set; }

        /// <summary>'x5c' collection (X.509 Certificate Chain)</summary>
        [JsonPropertyName("x5c")]
        public IList<string> X5c { get; set; }

        /// <summary>'x5t' (X.509 Certificate SHA-1 thumbprint)</summary>
        [JsonPropertyName("x5t")]
        public string? X5t { get; set; }

        /// <summary>'x5t#S256' (X.509 Certificate SHA-256 thumbprint)</summary>
        [JsonPropertyName("x5t#S256")]
        public string? X5tS256 { get; set; }

        /// <summary>'x5u' (X.509 URL)</summary>
        [JsonPropertyName("x5u")]
        public string? X5u { get; set; }

        /// <summary>'y' (ECC - Y Coordinate)</summary>
        [JsonPropertyName("y")]
        public string? Y { get; set; }
    }

}
