# guvensahin/TRYExchRate
T.C. Merkez Bankası Kur Kütüphanesi

## Nedir ?
T.C. Merkez Bankası tarafından yayınlanan döviz kurlarını almak için yazılan bir C# kütüphanesidir.

## Neler Yapılabilir ?
- Bu kütüphane ile belirttiğiniz tarihe ait döviz kurlarını çekebilirsiniz.
- TCMB sitesinde kuru yayınlanan tüm para birimlerini alabilirsiniz.
- TCMB sitesinde yayınlanan tüm kur tipleri için kullanabilirsiniz. Bunlar: Döviz alış, Döviz satış, Efektik alış ve Efektik satış'dır.

## Nasıl Kullanılır ?
Projeyi indirdiğinizde "TRYExchRate/src/TRYExchRate/TRYExchRate.cs" adresinde yer alan sınıfı kendi projenize dahil ettikten sonra aşağıdaki gibi çalıştırabilirsiniz.

```
TRYExchRate helper = new TRYExchRate(new DateTime(2016,12,30));
helper.LoadExchRate();

decimal usdExhRate = helper.GetExchRate("USD", ExchRateType.ForexBuying);
```

- "LoadExchRate" method'u çalıştığında TCMB'nin sitesinden ilgili tarihe ait tüm kurlar çekilir ve sınıfın içine kaydedilir.
- Bu aşamadan sonra "GetExchRate" method'u ile para birimi ve kur tipi belirterek çekilen kurları okuyabilirsiniz.

## Örnek
Projenin içinde sınıfın detaylı kullanımının gösterildiği örnek "Console Application" projesi bulunuyor. İndirdiğiniz projeyi visual studio ile açıp direkt run edebilirsiniz.

Örnek projenin çıktısı:

![sample](http://guvensahin.com/wp-content/uploads/2017/01/TRYExchRateSample.png)
