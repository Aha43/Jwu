using CommandLine;
using Jwu.Constants;
using Jwu.Methods;
using Jwu.Model.Parameters;

namespace Jwu.Cli;

internal class JwkBaseOptions
{
    [Option('v', "verbose", Required = false, HelpText = "Output information to console, optional: default is not to")]
    public bool Verbose { get; set; } = false;
}

[Verb("jwkcreate", HelpText = "Create private / public keys")]
internal class JwkCreateOptions : JwkBaseOptions
{
    [Option('n', "number", Required = false, HelpText = "Number of keys to create, optional: default is 1")]
    public int Number { get; set; } = 1;
    [Option("fpra", Required = false, HelpText = "Force json array output for private key when creating only one key")]
    public bool ForcePrivArray { get; set; } = false;
    [Option("fpua", Required = false, HelpText = "Force json array output for public key when creating only one key")]
    public bool ForcePubArray { get; set; } = false;
    [Option("fa", Required = false, HelpText = "Force json array output for both public and private key when creating only one key")]
    public bool ForceArray { get; set; } = false;
    [Option('d', Required = false, HelpText = "Destination for file to write keys to: 'private.json' is added to get file path for private keys and 'public.json' is added to get file path for public keys")]
    public string? Destination { get; set; } = null;
    [Option('a', "alg", Required = false, HelpText = "Algorithm keys are to be used with, optional: default is RS512")]
    public string? Alg { get; set; } = null;
}

internal static class JwkCreate
{
    public static int Perform(JwkCreateOptions o)
    {
        try
        {
            var forceArrayKeyPart = KeyPart.None;
            if (o.ForceArray || (o.ForcePrivArray && o.ForcePubArray))
            {
                forceArrayKeyPart = KeyPart.Both;
            }
            else if (o.ForcePrivArray)
            {
                forceArrayKeyPart = KeyPart.Private;
            }
            else if (o.ForcePubArray)
            {
                forceArrayKeyPart = KeyPart.Public;
            }

            var alg = o.Alg ?? Algorithm.RS512;

            var param = new CreateJwkParameter
            {
                ForceArray = forceArrayKeyPart,
                Alg = alg,
            };

            var (privJson, pubJson) = JwkMethods.CreateKeysJson(param, o.Number);

            if (o.Verbose)
            {
                Console.WriteLine("PRIVATE KEYS");
                Console.WriteLine(privJson);
                Console.WriteLine("PUBLIC KEYS");
                Console.WriteLine(pubJson);
            }

            if (o.Destination != null) 
            {
                var privateFile = o.Destination + ".private.json";
                var publicFile = o.Destination + ".public.json";

                File.WriteAllText(privateFile, privJson);
                File.WriteAllText(publicFile, pubJson);
            }

            return 0;
        }
        catch (Exception ex) 
        {
            Console.Error.WriteLine(ex);
            return 1;
        }

    }

}
