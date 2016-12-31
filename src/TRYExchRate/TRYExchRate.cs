using System;
using System.Xml;

namespace TRYExchRate
{
    /// <summary>
    /// T.C. Merkez Bankası tarafından yayınlanan döviz kurlarını okumak için kullanılan açık kaynak bir C# sınıfıdır.
    /// </summary>
    class TRYExchRate
    {
        private const string ApiBaseUrl = "http://www.tcmb.gov.tr/kurlar/{0}.xml";

        public string ApiUrl { get; set; }
        public DateTime CurrencyDate { get; set; }
        private XmlDocument XmlDoc { get; set; }

        public TRYExchRate(DateTime currencyDate)
        {
            CurrencyDate = currencyDate;
        }

        public Decimal GetExchRate(string currency, ExchRateType exchRateType)
        {
            LoadXmlDoc();

            System.Globalization.CultureInfo culInfo = new System.Globalization.CultureInfo("en-US", true);
            string nodeStr = String.Format("Tarih_Date/Currency[@CurrencyCode='{0}']/{1}", currency.ToUpper(), GetExchRateTypeNodeStr(exchRateType));

            return Decimal.Parse(XmlDoc.SelectSingleNode(nodeStr).InnerXml, culInfo);
        }


        private void GenerateApiUrl()
        {
            ApiUrl = String.Format(TRYExchRate.ApiBaseUrl, this.CurrencyDate.ToString("yyyyMM") + "/" + this.CurrencyDate.ToString("ddMMyyyy"));
        }

        public void LoadXmlDoc()
        {
            if (XmlDoc == null)
            {
                XmlDoc = new XmlDocument();

                try
                {
                    GenerateApiUrl();
                    XmlDoc.Load(ApiUrl);
                }
                catch
                {
                    throw new Exception("Cannot connect TCMB api right now. Api url: " + ApiUrl);
                }
            }
        }

        private string GetExchRateTypeNodeStr(ExchRateType exchRateType)
        {
            string ret = "";

            switch (exchRateType)
            {
                case ExchRateType.ForexBuying:
                    ret = "ForexBuying";
                    break;

                case ExchRateType.ForexSelling:
                    ret = "ForexSelling";
                    break;

                case ExchRateType.BanknoteBuying:
                    ret = "BanknoteBuying";
                    break;

                case ExchRateType.BanknoteSelling:
                    ret = "BanknoteSelling";
                    break;
            }

            return ret;
        }
    }

    public enum ExchRateType
    {
        ForexBuying,
        ForexSelling,
        BanknoteBuying,
        BanknoteSelling
    }
}
