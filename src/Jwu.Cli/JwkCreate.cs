using CommandLine;
using Jwu.Constants;
using Jwu.Exceptions;
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

    [Option("no-array-output", Required = false, HelpText = @"Tell non jwks format output: 
        string that does not contain 'u', 'r' or 'b': (default): array,
        string containing 'u': public key not in array if producing only one key, 
        string containing 'r': private key not in array if producing only one key, 
        string containing 'b': both public key and private not in array if producing only one key")]
    public string NoArrayOutput { get; set; } = string.Empty;

    [Option("jwks-output", Required = false, HelpText = @"Tell jwks format output:
        string that does not contain 'u', 'r' or 'b': (default): nor public or private keys in jwks format,
        string containing 'u': public keys in jwks format, 
        string containing 'r': private keys in jwks format, 
        string containing 'b': both public key and private keys in jwks format")]
    public string JwksOutput { get; set; } = string.Empty;

    [Option("pretty-output", Required = false, HelpText = @"Tell if to output pretty json:
        string that does not contain 'u, 'r', or 'b': (default): nor public or private keys in pretty format,
        string containing 'u': public keys in pretty format,
        string containing 'r': private keys in pretty format,
        string containing 'b': both public key and private keys in jwks format")]
    public string Pretty { get; set; } = string.Empty;

    [Option("kid", Required = false, HelpText = "If to create key id (kid) for generated keys. Optional: default is not to but kid is always created if output in the jwks format.")]
    public bool Kid { get; set; } = false;

    [Option('d', "destination", Required = false, HelpText = "Destination for file to write keys to: 'private.json' is added to get file path for private keys and 'public.json' is added to get file path for public keys")]
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
            var alg = o.Alg ?? Algorithm.RS512;

            var param = new CreateJwkParameter
            {
                ForceSingle = KeyPartFromCliArg(o.NoArrayOutput),
                Jwks = KeyPartFromCliArg(o.JwksOutput),
                PrettyJson = KeyPartFromCliArg(o.Pretty),
                IncludeKid = o.Kid,
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

                if (o.Verbose)
                {
                    Console.WriteLine("Private keys written to: " + privateFile);
                    Console.WriteLine("Public keys written to: " + publicFile);
                }

                if (!File.Exists(privateFile))
                {
                    File.Create(privateFile);
                }                

                if (!File.Exists(publicFile))
                {
                    
                    File.Create(publicFile);
                }

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

    private static KeyPart KeyPartFromCliArg(string cliArg)
    {
        if (string.IsNullOrEmpty(cliArg)) return KeyPart.None;
        if ((cliArg.IndexOf('u') != -1 && cliArg.IndexOf('r') != -1) || cliArg.IndexOf('b') != -1) return KeyPart.Both;
        if (cliArg.IndexOf('u') != -1) return KeyPart.Public;
        if (cliArg.IndexOf('r') != -1) return KeyPart.Private;
        return KeyPart.None;
    }

}
