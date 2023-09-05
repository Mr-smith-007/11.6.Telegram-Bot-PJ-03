using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _11._6.Telegram_Bot_PJ_03_.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace _11._6.Telegram_Bot_PJ_03_.Controllers
{
    public class InlineKeyController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");

            if (callbackQuery?.Data == null)
                return;

            
            _memoryStorage.GetSession(callbackQuery.From.Id).UserActionType = callbackQuery.Data;


            string actionText = callbackQuery.Data
                switch
            {
                "num" => " Количество",
                "sum" => " Сумма",
                _ => String.Empty
            };

            
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b> Выбрана команда - {actionText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
