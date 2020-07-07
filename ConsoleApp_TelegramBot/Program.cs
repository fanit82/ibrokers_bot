using System;
using System.Threading;
using Telegram.Bot;
using System.Net;
namespace ConsoleApp_TelegramBot
{
    class Program
    {
        static ITelegramBotClient botClient;
        //private static readonly TelegramBotClient bot = new TelegramBotClient("1206806449:AAHb5mWsgev-vl0R0qmN6PWzQQO_k75QiLM");       
        static void Main()
        {
            //var botClient = new TelegramBotClient("1206806449:AAHb5mWsgev-vl0R0qmN6PWzQQO_k75QiLM");
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            botClient = new TelegramBotClient("1206806449:AAHb5mWsgev-vl0R0qmN6PWzQQO_k75QiLM");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );
            //Console.ReadLine();
            botClient.OnMessage += BotClient_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
        private static async void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine("Chat name is:" + e.Message.Chat.FirstName);
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}  name: {e.Message.Chat.FirstName}  . ");
                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: char.ConvertFromUtf32(0x1F534) 
                  + char.ConvertFromUtf32(0x1F7E2)
                  + char.ConvertFromUtf32(0x1F534) 
                  + char.ConvertFromUtf32(0x1F7E2
                  )
                );
                //await Api.SendTextMessageAsync(message.Chat.Id, char.ConvertFromUtf32(0x1F601));
                //throw new NotImplementedException();
            }            
        }
    }
}
