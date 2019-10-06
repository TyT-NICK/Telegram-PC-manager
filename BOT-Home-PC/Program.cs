using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Telegram.Bot;

namespace BOT_Home_PC
{
    class Program
    {
        static TelegramBotAdapted bot;
        static void Main(string[] args)
        {
            bot = InitializeBot();
            if (bot != null)
            {
                bot.StartReceiving();
                while (Console.ReadKey().Key != ConsoleKey.PageDown) { }
                bot.StartReceiving();
            }
            else
                Console.WriteLine("Bot is not created");
        }

        //TODO: Разнсти инициализацию нового и существующего бота в разные методы
        public static TelegramBotAdapted InitializeBot()
        {
            TelegramBotAdapted bot = null;

            if (!Directory.Exists(@"bot"))
                Directory.CreateDirectory(@"bot");

            string pathToken = @"bot/bot-info.tbi";
            string pathUserList = @"bot/users.ul";
            string userInput = "";

            string token = "";
            string password = "";

            if (!File.Exists(pathToken))
            {
                while (bot == null)
                {
                    try
                    {
                        Console.WriteLine("Enter bot token (/cancel to exit): ");
                        userInput = Console.ReadLine();
                        if (userInput == "/cancel")
                            return null;
                        token = userInput;

                        Console.WriteLine("Enter bot password (/cancel to exit): ");
                        userInput = Console.ReadLine();
                        if (userInput == "/cancel")
                            return null;
                        password = userInput;
                        userInput = "";
                        while (userInput != password)
                        {
                            Console.WriteLine("Once more: ");
                            userInput = Console.ReadLine();
                            if (userInput == "/cancel")
                                return null;
                            else if (userInput != password)
                                Console.WriteLine("Entered passwords differ");
                        }

                        bot = new TelegramBotAdapted(token, password, new List<int>()) ;
                    }
                    catch (ArgumentException ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Console.WriteLine("The token is incorrect");
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(pathToken, FileMode.Create)))
                {
                    writer.Write(token);
                    writer.Write(password);
                }
            }
            else
            {
                List<int> userList = new List<int>();

                using (BinaryReader reader = new BinaryReader(File.Open(pathToken, FileMode.Open)))
                {
                    token = reader.ReadString();
                    password = reader.ReadString();
                }
                using (BinaryReader reader = new BinaryReader(File.Open(pathUserList, FileMode.Open)))
                {
                    while (true)
                    {
                        try
                        {
                            userList.Add(reader.ReadInt32());
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                bot = new TelegramBotAdapted(token, password, userList);
            }
            Console.Clear();

            Console.WriteLine("Bot initialized");
            return bot;
        }

        public static void WriteUserList(List<int> userList)
        {
            string fileName = @"bot/users.ul";

            using (BinaryWriter writer = new BinaryWriter(File.Create(fileName)))
            {
                foreach (var e in userList)
                    writer.Write(e);
            }
        }
    }
}
