using System;
using CommandLine;

namespace parser_my
{
    class Program
    {
        [Verb("getdate", HelpText = "gets name of todays day")]
        class GetDateOptions
        {
            [Option('t', "Tomorrow", SetName = "t", Required = false, HelpText = "Gets information about tomorrow instead of today")]
            public bool Tomorrow { get; set; }

            [Option('a', "AddDays", SetName = "a", Required = false, HelpText = "Gets information about specified future day")]
            public int AddDays { get; set; }

        }

        [Verb("getmonth", HelpText = "gets information about current month")]
        class GetMonthOptions { }

        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<GetDateOptions, GetMonthOptions>(args);

            result.WithParsed<GetDateOptions>(opts =>
            {
                if (opts.Tomorrow) {
                    Console.WriteLine("Tomorrow is {0}", DateTime.Now.AddDays(1).DayOfWeek.ToString());
                } else if (opts.AddDays != 0) {
                    Console.WriteLine("In {0} days it is {1}", opts.AddDays, DateTime.Now.AddDays(opts.AddDays).DayOfWeek.ToString());
                } else {
                    Console.WriteLine("Today is {0}", DateTime.Now.DayOfWeek.ToString());
                }
            });

            result.WithParsed<GetMonthOptions>(opts =>
            {
                Console.WriteLine("Today is month {0}", DateTime.Now.ToString("MMMM"));
            });

        }
    }
}
