using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace BOT_Home_PC
{
    class TelegramBotAdapted : TelegramBotClient
    {
        /*
         * 
         * static WebProxy proxy = new WebProxy("14.188.41.69", 8080);
        static TelegramBotClient bot = new TelegramBotClient("694238993:AAHJZVyK6IvQ2lFxyuH-vepfEuqpBWqk5WA", proxy);
         * */
        public TelegramBotAdapted(string token, System.Net.WebProxy proxy) : base(token, proxy) { }
    }
}
