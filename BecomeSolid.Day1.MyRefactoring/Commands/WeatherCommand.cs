using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace BecomeSolid.Day1.MyRefactoring
{
    public class WeatherCommand : ICommand
    {
        private WeatherService _weatherService;
        private ILogger _log;

        public WeatherCommand(ILogger log)
        {
            _weatherService = new WeatherService();
            _log = log;
        }

        public async Task Execude(Telegram.Bot.Types.Update update, TelegramBotClient bot)
        {
            var command = update.Message.Text.Split(new char[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (command.Count() <= 1)
            {
                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Не введен город");
                _log.Information("Не введен город");
                return;
            }
            string city = command[1];
            try
            {
                var weather = _weatherService.Get(city);
                await bot.SendTextMessageAsync(update.Message.Chat.Id, String.Format($"В {weather.City} {weather.Description} и температура {weather.Temperature.ToString("+#;-#;0")} °C"));
                _log.Information(String.Format($"В {weather.City} {weather.Description} и температура {weather.Temperature.ToString("+#;-#;0")} °C"));

            }
            catch (WeatherException ex)
            {
                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Ошибка получения погоды");
                _log.Information("Ошибка получения погоды");

            }
        }

        public bool IsApplicable(Update update)
        {
            if (update.Message.Text.Contains("/weather"))
                return true;
            return false;
        }


    }
}
