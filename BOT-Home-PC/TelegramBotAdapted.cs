using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace BOT_Home_PC
{
    class TelegramBotAdapted : TelegramBotClient
    {
        //public static Dictionary validatedUserList

        public static WebProxy currentProxy = new WebProxy("14.188.41.69", 8080);

        private string Password { get; set; }
        private List<int> ValidatedUsers { get; set; }
        private Dictionary<string, string> HelpList { get; set; }

        public TelegramBotAdapted(string token, string password, List<int> userList) : base(token, currentProxy) 
        {
            this.Password = password;
            this.ValidatedUsers = userList;
            
            this.OnMessage += BotMessageRecieved;

        }

        private void BotMessageRecieved(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Debug.WriteLine($"{e.Message.From.FirstName} {e.Message.From.LastName} ({e.Message.From.Id}): [{e.Message.Type.ToString()}] {e.Message.Text}");
            
            //TODO: Обработка нетекстового сообщения
            switch (e.Message.Text.Trim())
            {
                case "/start":
                    if (ValidatedUsers.Contains(e.Message.From.Id))
                    { 
                        this.SendTextMessageAsync(e.Message.Chat.Id, "You are already authorized");
                    }
                    else
                    {
                        this.SendTextMessageAsync(e.Message.Chat.Id, "New user! Enter password to continue");
                        this.OnMessage -= BotMessageRecieved;
                        this.OnMessage += ValidationProcess;
                    }
                    break;
                case "/testcommand":
                    this.SendTextMessageAsync(e.Message.Chat.Id, "/testcommand");
                    break;
                case "/gettestfile":
                    this.SendDocumentAsync(e.Message.Chat.Id, File.Open("self.jpg", FileMode.Open));
                    break;
            }
        }

        private void ValidationProcess(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Text == this.Password)
            {
                this.SendTextMessageAsync(e.Message.Chat.Id, "Correct Password");
                this.ValidatedUsers.Add(e.Message.From.Id);
                this.OnMessage -= ValidationProcess;
                this.OnMessage += BotMessageRecieved;
                Program.WriteUserList(this.ValidatedUsers);
            }
            else
            {
                this.SendTextMessageAsync(e.Message.Chat.Id, "Wrong Password. Access denied");
            }
        }
    }
}
