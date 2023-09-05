using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._6.Telegram_Bot_PJ_03_.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
