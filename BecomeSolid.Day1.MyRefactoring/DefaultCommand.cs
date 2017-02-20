using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BecomeSolid.Day1.MyRefactoring
{
    public class DefaultCommand : ICommand
    {
        private ILogger _log;

        public DefaultCommand(ILogger log)
        {
            _log = log;

        }
        public async Task Execude(Telegram.Bot.Types.Update update, TelegramBotClient bot)
        {
            await bot.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
            await Task.Delay(2000);
            var t = await bot.SendTextMessageAsync(update.Message.Chat.Id, update.Message.Text);
            _log.Information(update.Message.Text);
        }

        public bool IsApplicable(Telegram.Bot.Types.Update update)
        {
            return true;
        }
    }
}
