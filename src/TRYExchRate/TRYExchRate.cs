using System;
using System.Net;
using System.Xml;

namespace TRYExchRate
{
    /// <summary>
    /// T.C. Merkez Bankası (TCMB) tarafından yayınlanan döviz kurlarını alır
    /// Güven Şahin - guvensahin.com
    /// </summary>
    class TRYExchRate
    {
        /// <summary>
        /// TCMB base api linki
        /// </summary>
        private const string ApiBaseUrl = "http://www.tcmb.gov.tr/kurlar/{0}.xml";

        /// <summary>
        /// Kullanıcının belirttiği tarihte kur yok ise geçmişe doğru kontrol edilecek gün sayısı
        /// </summary>
        private const int ExchRateAttempts = 5;

        /// <summary>
        /// Kurları çekme işlemi için kullanılan, tarih bilgisi eklenmiş api linki
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Kur'un çekilmek istendiği tarih
        /// </summary>
        public DateTime CurrencyDate { get; set; }

        /// <summary>
        /// TCMB'den kurun alındığı gerçek tarih
        /// </summary>
        public DateTime ActualCurrencyDate { get; set; }

        /// <summary>
        /// Çekilen kur verisinin saklandığı değişken
        /// </summary>
        private XmlDocument XmlDoc { get; set; }


        /// <summary>
        /// </summary>
        /// <param name="currencyDate">Kurun alınacağı tarihi</param>
        public TRYExchRate(DateTime currencyDate)
        {
            CurrencyDate = currencyDate;
        }

        /// <summary>
        /// Kur'u getirir
        /// </summary>
        /// <param name="currency">Kurun alınmak istendiği para birimi</param>
        /// <param name="exchRateType">Alınmak istenen kur tipi</param>
        /// <returns></returns>
        public Decimal GetExchRate(string currency, ExchRateType exchRateType)
        {
            // eğer daha önce load edilmemiş ise bu aşamada yapılır
            if (XmlDoc == null)
            {
                LoadExchRate();
            }

            // TCMB noktayı (.) ondalık ayracı olarak kullanıyor.
            // string'den decimal'e çevrim sırasında windows region ayarlarından etkilenmeden doğru çevrilmesi için en-us culture'ı kullanılır
            System.Globalization.CultureInfo culInfo = new System.Globalization.CultureInfo("en-US", true);

            // xml içinde okunacak node ayarlanır
            string nodeStr = String.Format("Tarih_Date/Currency[@CurrencyCode='{0}']/{1}", currency.ToUpper(), GetExchRateTypeNodeStr(exchRateType));

            // string olarak alınan kur decimal'e çevrilip dönülür
            return Decimal.Parse(XmlDoc.SelectSingleNode(nodeStr).InnerXml, culInfo);
        }


        /// <summary>
        /// Belirtilen tarihe göre kullanılacak api url'i oluşturulur
        /// </summary>
        private void GenerateApiUrl()
        {
            ApiUrl = String.Format(TRYExchRate.ApiBaseUrl, this.ActualCurrencyDate.ToString("yyyyMM") + "/" + this.ActualCurrencyDate.ToString("ddMMyyyy"));
        }


        /// <summary>
        /// Belirtilen tarihteki TCMB'deki bütün kurları çeker
        /// </summary>
        public void LoadExchRate()
        {
            ActualCurrencyDate = CurrencyDate;

            // kullanıcının belirttiği tarihte kur var ise alınır
            // yok ise en yakın kur olan gün bulunur
            for (int attempts = 0; attempts <= TRYExchRate.ExchRateAttempts; attempts++)
            {
                try
                {
                    GenerateApiUrl();

                    XmlDoc = new XmlDocument();
                    XmlDoc.Load(ApiUrl);

                    break;
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        // 404 not found
                        HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                        if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                        {
                            // bir gün öncesi kontrol edilir
                            ActualCurrencyDate = ActualCurrencyDate.AddDays(-1);
                        }
                        else
                        {
                            throw new Exception("Kur bilgisi bulunamadı.");
                        }
                    }
                    else
                    {
                        throw new Exception("Kur bilgisi bulunamadı.");
                    }
                }
            }

            if (XmlDoc == null)
            {
                throw new Exception("Kur bilgisi bulunamadı.");
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


    /// <summary>
    /// Kur tipleri
    /// </summary>
    public enum ExchRateType
    {
        /// <summary>
        /// Döviz Alış
        /// </summary>
        ForexBuying,

        /// <summary>
        /// Döviz Satış
        /// </summary>
        ForexSelling,

        /// <summary>
        /// Efektif Alış
        /// </summary>
        BanknoteBuying,

        /// <summary>
        /// Efektif Satış
        /// </summary>
        BanknoteSelling
    }
}
