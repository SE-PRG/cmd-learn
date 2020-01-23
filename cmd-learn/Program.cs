using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace cmd_learn
{
    class Program
    {
        static int Main(string[] args)
        {
            
            //
            // Create a root command and subcommands
            //
            var rootCommand = new RootCommand("CLI arguments learning app");
            var monthCommand = new Command("getmonth", "gets information about current month");
            var dayCommand = new Command("getday", "gets name of todays day")
            {
                new Option("--Tomorrow", "Gets information about tomorrow instead of today") {Argument = new Argument<bool>()}
            };
            
            rootCommand.AddCommand(dayCommand);
            rootCommand.AddCommand(monthCommand);

            //
            // add command handlers
            //
            rootCommand.Handler = CommandHandler.Create<bool>((boolOption) =>
            {
                Console.WriteLine("ahoj");
                // TODO: Change this to description
                Console.WriteLine(rootCommand.Description.ToString());
                Console.WriteLine(rootCommand.ToString());
            });

            dayCommand.Handler = CommandHandler.Create<bool>((tomorrow) =>
            {
                if (tomorrow) {
                    Console.WriteLine("Tomorrow is {0}", DateTime.Now.AddDays(1).DayOfWeek.ToString());
                } else {
                    Console.WriteLine("Today is {0}", DateTime.Now.DayOfWeek.ToString());
                }
            });

            monthCommand.Handler = CommandHandler.Create(() =>
            {
                Console.WriteLine("Today is month {0}", DateTime.Now.ToString("MMMM"));
            });


            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
