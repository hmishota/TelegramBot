using BecomeSolid.Day1.MyRefactoring.Commands;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BecomeSolid.Day1.MyRefactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");

            var log = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.LiterateConsole(LogEventLevel.Information)
               .WriteTo.RollingFile("log1-{Date}.txt", LogEventLevel.Information)
               .CreateLogger();
            Log.Logger = log;
            Run().Wait();
        }

        static async Task Run()
        {
            var bot = new TelegramBotClient("353997582:AAFfZJRBIbt1dGNQDIjWUS5nXBV367LyfXI");
            var me = await bot.GetMeAsync();

            ICommand weatherCommand = new WeatherCommand(Log.Logger);
            ICommand quoteCommand = new QuoteCommand(Log.Logger);
            ICommand defaultCommand = new DefaultCommand(Log.Logger);

            ICommand[] commands = new ICommand[] { weatherCommand, quoteCommand, defaultCommand };

            Console.WriteLine("Hello my name is {0}", me.Username);

            var offset = 0;

            while (true)
            {
                var updates = await bot.GetUpdatesAsync(offset);

                foreach (var update in updates)
                {
                    foreach(var command in commands )
                    {
                        if (command.IsApplicable(update))
                        {
                            command.Execude(update, bot).Wait();
                            break;
                        }
                    }

                    offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }
        }

    }
}
