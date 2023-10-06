using CommandLine;
using Jwu.Methods;
using Jwu.Model.Parameters;

namespace Jwu.Cli;

[Verb("jwkcreate", HelpText = "Create private / public keys")]
internal class JwkCreateOptions
{
    [Option('n', "Number", Required = false, HelpText = "Number of keys to create, optional: default is 1")]
    public int Number { get; set; } = 1;

}

internal static class JwkCreate
{
    public static int Perform(JwkCreateOptions o)
    {
        var param = new CreateJwkParameter
        {

        };

        var (privJson, pubJson) = Jwk.CreateKeysJson(param, o.Number);
        Console.WriteLine("PRIVATE KEYS");
        Console.WriteLine(privJson);
        Console.WriteLine("PUBLIC KEYS");
        Console.WriteLine(pubJson);

        return 0;
    }
}
