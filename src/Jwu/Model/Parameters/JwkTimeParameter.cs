using Jwu.Constants;

namespace Jwu.Model.Parameters
{
    public struct JwkTimeParameter
    {
        public JwkTimeParameter() { }

        public JwtTime ExpTime { get; set; }
        public KeyPart KeyPart { get; set; } = KeyPart.Public;
    }
}
