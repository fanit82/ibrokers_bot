using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_TelegramBot.Ibrockers
{
    public class DataToken //token hthong tra ve
    {
        public DateTime expiredDate { get; set; }
        public string token { get; set; }

    }
    public class LoginTokenModel //models khi login vao he thong
    {
        public DataToken data { get; set; }
        public int errorCode { get; set; }
        public string errorMessage { get; set; }

    }

    public class PriceModel
    {
        public string symbol { get; set; }
        public double open { get; set; }
        public double close { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public DateTime candleTime { get; set; }
        public int result { get; set; }

    }

    public class ResultPriceModel
    {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public List<PriceModel> data { get; set; }

    }
}
