using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_TelegramBot.Ibrockers
{
    class IbrokersProcess
    {
        public static async Task<String> GetToken(string strLiveID, string strLivePass)
        {
            var ObjPost = new { AccountName = strLiveID, Password = strLivePass };
            //
            using (var client = new HttpClient())
            {
                var PostContent = new StringContent(JsonConvert.SerializeObject(ObjPost), Encoding.UTF8, "application/json");
                HttpResponseMessage respose = await client.PostAsync(new Uri("https://api.ibrokers.io/api/Account/Login"), PostContent);
                if (respose.IsSuccessStatusCode)
                {
                    var JsonString = await respose.Content.ReadAsStringAsync();
                    LoginTokenModel LoginTK = JsonConvert.DeserializeObject<LoginTokenModel>(JsonString);
                    return LoginTK.data.token;
                    //response = result.StatusCode.ToString();
                }
                else
                {
                    throw new Exception(respose.ReasonPhrase);
                }
            }
        }

        public static async Task<List<PriceModel>> GetPriceCandlesticks(string strtoken)
        {
            using (var client = new HttpClient())
            {
                //var PostContent = new StringContent(JsonConvert.SerializeObject(ObjPost), Encoding.UTF8, "application/json");

                //client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Token", strtoken);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + strtoken);
                HttpResponseMessage respose = await client.GetAsync("https://api.ibrokers.io/api/Account/CandleChartGetPrice?NumOfCandle=10");

                if (respose.IsSuccessStatusCode)
                {
                    var JsonString = await respose.Content.ReadAsStringAsync();
                    ResultPriceModel Result = JsonConvert.DeserializeObject<ResultPriceModel>(JsonString);
                    return Result.data;
                    //LoginTokenModel Result = await respose.Content.ReadAsAsync<LoginTokenModel>();
                    //return Result.data;
                    //response = result.StatusCode.ToString();
                }
                else
                {
                    throw new Exception(respose.ReasonPhrase);
                }
            }
        }

        public static async Task<String> BetProcessAsync(String strToken, int BetType, int BetAmount)
        {
            var ResultBet = new {errorCode = 0,errorMessage = "" };
            using (var client = new HttpClient())
            {
                //var PostContent = new StringContent(JsonConvert.SerializeObject(ObjPost), Encoding.UTF8, "application/json");
                var ObjPost = new { Amount = BetAmount , BetType= BetType};
                var PostContent = new StringContent(JsonConvert.SerializeObject(ObjPost), Encoding.UTF8, "application/json");
                HttpResponseMessage respose = await client.PostAsync(new Uri("https://api.ibrokers.io/api/Account/Login"), PostContent);
                if (respose.IsSuccessStatusCode)
                {
                    ////var JsonString = await respose.Content.ReadAsStringAsync();
                    ////ResultBet BetResult  = JsonConvert.DeserializeObject<ResultBet>(JsonString);
                    ////return LoginTK.data.token;
                    ////response = result.StatusCode.ToString();
                }
                else
                {
                    throw new Exception(respose.ReasonPhrase);
                }
            }
        }
    }
}
