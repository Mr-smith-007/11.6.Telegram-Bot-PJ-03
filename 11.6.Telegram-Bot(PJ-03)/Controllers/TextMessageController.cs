using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using _11._6.Telegram_Bot_PJ_03_.Services;
using System.Threading;


namespace _11._6.Telegram_Bot_PJ_03_.Controllers
{
    public class TextMessageController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");

            switch (message.Text)
            {
                case "/start":

                    
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Количество" , $"num"),
                        InlineKeyboardButton.WithCallbackData($" Сумма" , $"sum")
                    });

                    
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Бот считает количествово символов в тексте или сумму чисел (вводить через пробел).</b> {Environment.NewLine}", 
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;

                
                default:

                    if (_memoryStorage.GetSession(message.Chat.Id).UserActionType == "num")
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Длина сообщения: {message.Text.Length} знаков", cancellationToken: ct);
                    else if (_memoryStorage.GetSession(message.Chat.Id).UserActionType == "sum")
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, Summator.Sum(message), cancellationToken: ct);
                    }

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Введите данные", cancellationToken: ct);
                    break;
            }
        }
    }
}
