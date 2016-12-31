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
            var exchRateHelper = new TRYExchRate(DateTime.Now);

            decimal test = exchRateHelper.GetExchRate("USD", ExchRateType.ForexBuying);


            Console.Write(exchRateHelper.CurrencyDate);
            Console.WriteLine("");
            Console.WriteLine("");

            // USD
            Console.WriteLine("USD");
            Console.WriteLine("Forex Buying: " + exchRateHelper.GetExchRate("USD", ExchRateType.ForexBuying).ToString());
            Console.WriteLine("Forex Selling: " + exchRateHelper.GetExchRate("USD", ExchRateType.ForexSelling).ToString());
            Console.WriteLine("Banknote Buying: " + exchRateHelper.GetExchRate("USD", ExchRateType.BanknoteBuying).ToString());
            Console.WriteLine("Banknote Selling: " + exchRateHelper.GetExchRate("USD", ExchRateType.BanknoteSelling).ToString());
            Console.WriteLine("");

            // Other
            Console.WriteLine("EUR - Forex Buying: " + exchRateHelper.GetExchRate("EUR", ExchRateType.ForexBuying).ToString());
            Console.WriteLine("GBP - Forex Buying: " + exchRateHelper.GetExchRate("GBP", ExchRateType.ForexBuying).ToString());
            Console.WriteLine("CAD - Forex Buying: " + exchRateHelper.GetExchRate("CAD", ExchRateType.ForexBuying).ToString());

            Console.ReadLine();
        }
    }
}
