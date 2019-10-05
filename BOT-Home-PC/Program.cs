using System;
using Telegram.Bot;
using System.Net;

namespace BOT_Home_PC
{
    class Program
    {
        
        static WebProxy proxy = new WebProxy("14.188.41.69", 8080);
        static TelegramBotAdapted bot = new TelegramBotAdapted("", proxy);
        static void Main(string[] args)
        {
            bot.OnMessage += BotOnMessageRecieved;
            bot.StartReceiving();
            while (Console.ReadKey().Key != ConsoleKey.PageDown) { }
            bot.StopReceiving();
        }

        static void BotOnMessageRecieved(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

        }
    }
}
