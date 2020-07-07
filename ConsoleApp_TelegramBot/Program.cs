using System;
using System.Threading;
using Telegram.Bot;
using System.Net;
namespace ConsoleApp_TelegramBot
{
    class Program
    {
        static long TelegramUserID = 0;
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
            while (true)
            {

            }
            //Thread.Sleep(int.MaxValue);
        }
        private static async void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                TelegramUserID = e.Message.Chat.Id;
                Console.WriteLine("Chat name is:" + e.Message.Chat.FirstName);
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}  name: {e.Message.Chat.FirstName}  . ");
                string strText = char.ConvertFromUtf32(0x1F534) + char.ConvertFromUtf32(0x1F7E2) + char.ConvertFromUtf32(0x1F534);
                await botClient.SendTextMessageAsync(e.Message.Chat,text: strText);                
                //await Api.SendTextMessageAsync(message.Chat.Id, char.ConvertFromUtf32(0x1F601));
                //throw new NotImplementedException();
            }            
        }

        private async void SendPayout(long TlgUserID)
        {
            string strText = char.ConvertFromUtf32(0x1F534) + char.ConvertFromUtf32(0x1F7E2) + char.ConvertFromUtf32(0x1F534);
            await botClient.SendTextMessageAsync(TlgUserID,strText);
        }

    }
}
