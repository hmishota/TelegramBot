using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BecomeSolid.Day1.MyRefactoring
{
    public interface ICommand
    {
        Task Execude(Telegram.Bot.Types.Update update, TelegramBotClient bot);
        bool IsApplicable(Telegram.Bot.Types.Update update);
    }
}
