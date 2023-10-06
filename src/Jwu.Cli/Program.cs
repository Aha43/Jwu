using CommandLine;
using Jwu.Cli;
using System.Reflection;

var types = LoadVerbs();

Parser.Default.ParseArguments(args, types)
        .WithParsed(Run);

static void Run(object obj)
{
    var exit = 0;
    switch (obj)
    {
        case JwkCreateOptions o: exit = JwkCreate.Perform(o); break;
    }

    Environment.Exit(exit);
}

static Type[] LoadVerbs() => Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();


