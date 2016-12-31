# guvensahin/TRYExchRate
T.C. Merkez Bankası Kur Kütüphanesi (C#)

## Nedir ?
T.C. Merkez Bankası tarafından yayınlanan döviz kurlarını almak için yazılan bir C# kütüphanesidir.

## Neler Yapabiliyor ?
- Bu kütüphane ile belirttiğiniz tarihe ait döviz kurlarını çekebilirsiniz.
- TCMB sitesinde kuru yayınlanan tüm para birimlerini alabilirsiniz.
- TCMB sitesinde yayınlanan tüm kur tipleri için kullanabilirsiniz. Bunlar: Döviz alış, Döviz satış, Efektik alış ve Efektik satış'dır.
- Eğer belirttiğiniz tarih TCMB'nin kur yayınlamadığı bir tarih ise (resmi tatil, hafta sonu, yarım iş günü vb) sınıf kur yayınlanan en yakın tarihi otomatik seçer.

## Nasıl Kullanılır ?
Projenin içinde kur sınıfı ve bu sınıfı kullanan örnek console application projesi bulunuyor.

"src/TRYExchRate/TRYExchRate.cs" adresinde yer alan sınıfı kendi projenize dahil ettikten sonra aşağıdaki gibi çalıştırabilirsiniz.

```cs
TRYExchRate helper = new TRYExchRate(new DateTime(2016,12,30));
helper.LoadExchRate();

decimal usdExhRate = helper.GetExchRate("USD", ExchRateType.ForexBuying);
```

- "LoadExchRate" method'u çalıştığında TCMB'nin sitesinden ilgili tarihe ait tüm kurlar çekilir ve sınıfın içine kaydedilir.
- Bu aşamadan sonra "GetExchRate" method'u ile para birimi ve kur tipi belirterek çekilen kurları okuyabilirsiniz.



## Örnek
İndirdiğiniz örnek projeyi visual studio ile açıp direkt run edebilirsiniz. Run ettiğinizde aşağıdaki gibi bir çıktı alacaksınız:

![sample](http://guvensahin.com/wp-content/uploads/2017/01/TRYExchRateSample.png)
