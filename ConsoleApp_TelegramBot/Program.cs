using System;
using System.Threading;
using Telegram.Bot;
using System.Net;
using ConsoleApp_TelegramBot.Ibrockers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleApp_TelegramBot
{
    class Program
    {
        static List<long> ListUserSign = new List<long>();
        static ITelegramBotClient botClient;
        static string PublicToken = string.Empty; //token de lay thong tin tu server. 
        //private static readonly TelegramBotClient bot = new TelegramBotClient("1206806449:AAHb5mWsgev-vl0R0qmN6PWzQQO_k75QiLM");       
        static async Task Main()
        {
            //var botClient = new TelegramBotClient("1206806449:AAHb5mWsgev-vl0R0qmN6PWzQQO_k75QiLM");
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            botClient = new TelegramBotClient("1206806449:AAHb5mWsgev-vl0R0qmN6PWzQQO_k75QiLM"); //lay bot telegram.
            botClient.OnMessage += BotClient_OnMessage;
            botClient.StartReceiving();
            //lay token.
            Task<string> strToken = IbrokersProcess.GetToken("D116771", "123456789");
            PublicToken = await strToken;
            while (true)
            {
                List<PriceModel> ListPayout = await IbrokersProcess.GetPriceCandlesticks(PublicToken);
                string strPayout = string.Empty;
                //char.ConvertFromUtf32(0x1F534) + char.ConvertFromUtf32(0x1F7E2) + char.ConvertFromUtf32(0x1F534);

                foreach (PriceModel item in ListPayout)
                {
                    if (item.close > item.open) //Buy
                    {
                        strPayout = char.ConvertFromUtf32(0x1F7E2) + strPayout;
                    }
                    else //sell
                    {
                        strPayout = char.ConvertFromUtf32(0x1F534) + strPayout;
                    }
                }
                if (ListUserSign.Count>0)
                {
                    foreach (long UserChatID in ListUserSign)
                    {
                        SendPayout(UserChatID, strPayout);
                    }                    
                    await Task.Delay(60 * 1000);
                }

            }
            //Thread.Sleep(int.MaxValue);

            //Console.WriteLine(strABC);

            //Console.ReadLine();

            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //botClient = new TelegramBotClient("1206806449:AAHb5mWsgev-vl0R0qmN6PWzQQO_k75QiLM");
            //var me = botClient.GetMeAsync().Result;
            //Console.WriteLine(
            //  $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            //);
            ////Console.ReadLine();

            //while (true)
            //{

            //}
            //Thread.Sleep(int.MaxValue);
        }
        private static async void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            switch (e.Message.Text.ToLower())
            {
                case "/sign_on": //dang ky nhan tin hieu.
                    ListUserSign.Add(e.Message.Chat.Id);
                    Console.WriteLine("User " + e.Message.Chat.Id.ToString() + " đăng ký nhận tín hiệu");
                    await botClient.SendTextMessageAsync(e.Message.Chat, text: "Bạn đăng ký nhận tín hiệu thành công");
                    break;
                case "/sign_off": //tắt tín hiệu
                    ListUserSign.RemoveAt(ListUserSign.FindIndex(x=>x== (e.Message.Chat.Id)));
                    Console.WriteLine("User " + e.Message.Chat.Id.ToString() + " bỏ nhận tín hiệu");
                    await botClient.SendTextMessageAsync(e.Message.Chat, text: "Bạn bỏ nhận tín hiệu");
                    break;
                default:
                    await botClient.SendTextMessageAsync(e.Message.Chat, text: "Lệnh không phù hợp!");
                    break;
            }           
        }
        private static async void SendPayout(long TlgUserID,string strPayout)
        {            
            await botClient.SendTextMessageAsync(TlgUserID, strPayout);
        }

    }
}
