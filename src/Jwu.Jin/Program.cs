
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

var jsonPath = args[1];
var json = File.ReadAllText(jsonPath);
var param = JsonSerializer.Deserialize<CreateJwkParameter>(json);

if (param == null)
{
    Console.WriteLine("Could not deserialize json from: " + jsonPath);
    return;
}

switch (command)
{     case "Create":
        var (priv, pub) = JwkMethods.CreateKeys(param, param.Number);
        var privJson = JsonSerializer.Serialize(priv, new JsonSerializerOptions { WriteIndented = param.PrettyJson == KeyPart.Private });
        var pubJson = JsonSerializer.Serialize(pub, new JsonSerializerOptions { WriteIndented = param.PrettyJson == KeyPart.Public });

        if (param.ForceSingle == KeyPart.None)
        {
            File.WriteAllText("private.json", privJson);
            File.WriteAllText("public.json", pubJson);
        }
        else
        {
            File.WriteAllText("key.json", privJson);
        }
        break;
    default:
        Console.WriteLine("Unknown command: " + command);
        break;
}
