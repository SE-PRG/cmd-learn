using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

namespace firstApp
{
    class Program
    {
        static int Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
    {
        new Option(
            "--int-option",
            "An option whose argument is parsed as an int")
        {
            Argument = new Argument<int>(defaultValue: () => 42)
        },
        new Option(
            "--bool-option",
            "An option whose argument is parsed as a bool")
        {
            Argument = new Argument<bool>()
        },
        new Option(
            "--file-option",
            "An option whose argument is parsed as a FileInfo")
        {
            Argument = new Argument<FileInfo>()
        }
    };

            rootCommand.Description = "My sample app";

            rootCommand.Handler = CommandHandler.Create<int, bool, FileInfo>((intOption, boolOption, fileOption) =>
            {
                Console.WriteLine($"The value for --int-option is: {intOption}");
                Console.WriteLine($"The value for --bool-option is: {boolOption}");
                Console.WriteLine($"The value for --file-option is: {fileOption?.FullName ?? "null"}");
            });

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
