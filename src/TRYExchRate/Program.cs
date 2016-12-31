using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRYExchRate
{
    class Program
    {
        static void Main(string[] args)
        {
            TRYExchRate helper = new TRYExchRate(DateTime.Now);
            helper.LoadExchRate();

            Console.Write("İstenilen kur tarihi: " + helper.CurrencyDate);
            Console.WriteLine("");
            Console.Write("Alınan kur tarihi: " + helper.ActualCurrencyDate);
            Console.WriteLine("");
            Console.WriteLine("Api linki: " + helper.ApiUrl);
            Console.WriteLine("");

            // USD
            Console.WriteLine("USD - Döviz Alış: " + helper.GetExchRate("USD", ExchRateType.ForexBuying).ToString());
            Console.WriteLine("USD - Döviz Satış: " + helper.GetExchRate("USD", ExchRateType.ForexSelling).ToString());
            Console.WriteLine("USD - Efektif Alış: " + helper.GetExchRate("USD", ExchRateType.BanknoteBuying).ToString());
            Console.WriteLine("USD - Efektif Satış: " + helper.GetExchRate("USD", ExchRateType.BanknoteSelling).ToString());
            Console.WriteLine("");

            // diğer para birimleri
            Console.WriteLine("EUR - Döviz Alış: " + helper.GetExchRate("EUR", ExchRateType.ForexBuying).ToString());
            Console.WriteLine("GBP - Döviz Alış: " + helper.GetExchRate("GBP", ExchRateType.ForexBuying).ToString());
            Console.WriteLine("CAD - Döviz Alış: " + helper.GetExchRate("CAD", ExchRateType.ForexBuying).ToString());

            Console.ReadLine();
        }
    }
}