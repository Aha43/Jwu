
using Jwu.Constants;
using Jwu.Methods;
using Jwu.Model.Parameters;
using System.Text.Json;

if (args.Length < 1)
{
    Console.WriteLine("Need one or two arguments: First Command (Create) second path to json second is optional path to specification file in json format");
    return;
}

if (args.Length > 2)
{
    Console.WriteLine("Too many arguments: First Command (Create) second path to json second is optional path to specification file in json format");
    return;
}

var command = args[0];
if (command != "Create")
{
    Console.WriteLine("Unknown command: " + command);
    return;
}

CreateJwkParameter param = new();

if (args.Length == 2)
{
    var jsonPath = args[1];
    var json = File.ReadAllText(jsonPath);
    var deserialized = JsonSerializer.Deserialize<CreateJwkParameter>(json);
    if (deserialized == null)
    {
        Console.WriteLine("Failed to parse json");
        return;
    }

    param = deserialized;
}

var jso = new JsonSerializerOptions { WriteIndented = true };

switch (command)
{     case "Create":
        var (priv, pub) = JwkMethods.CreateKeys(param, param.Number);
        var privJson = JsonSerializer.Serialize(priv, jso);
        var pubJson = JsonSerializer.Serialize(pub, jso);

        if (param.PrivateKeysOutputFilePath != null)
        {
            File.WriteAllText(param.PrivateKeysOutputFilePath, privJson);
        }
        if (param.PublicKeysOutputFilePath != null)
        {
            File.WriteAllText(param.PublicKeysOutputFilePath, pubJson);
        }
        break;
    default:
        Console.WriteLine("Unknown command: " + command);
        break;
}

