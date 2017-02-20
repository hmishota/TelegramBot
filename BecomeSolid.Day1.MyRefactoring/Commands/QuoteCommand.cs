using BecomeSolid.Day1.MyRefactoring.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BecomeSolid.Day1.MyRefactoring.Commands
{
    public class QuoteCommand : ICommand
    {
        private QuoteService _quoteService;
        private ILogger _log;

        public QuoteCommand(ILogger log)
        {
            _quoteService = new QuoteService();
            _log = log;
        }

        public async Task Execude(Update update, TelegramBotClient bot)
        {
            try
            {
                var quote = _quoteService.Get();
                await bot.SendTextMessageAsync(update.Message.Chat.Id, String.Format($"{quote.QuoteText} Aвтор: {quote.QuoteAuthor}"));
                _log.Information(String.Format($"{quote.QuoteText} Aвтор: {quote.QuoteAuthor}"));

            }
            catch (WeatherException ex)
            {
                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Ошибка получения цитаты");
                _log.Information("Ошибка получения цитаты");
            }
        }

        public bool IsApplicable(Update update)
        {
            if (update.Message.Text.Contains("/quote"))
                return true;
            return false;
        }
    }
}
