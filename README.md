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
var exchRateHelper = new TRYExchRate(DateTime.Now);

// bu aşamada tcmb den belirttiğiniz tarih için kur çekilir.
// ilgili tarih'e ait bütün para birimlerini ve kur tiplerini çekip, sınıf içinde saklar.
decimal usdExhRate = exchRateHelper.GetExchRate("USD", ExchRateType.ForexBuying);

// aynı instance ile farklı işlem yaptığınızda kuru tekrar çekmeye gerek yoktur. bu sebeple önceden kaydettiğini okur.
decimal eurExhRate = exchRateHelper.GetExchRate("EUR", ExchRateType.ForexSelling);
```

Projenin içinde sınıfın detaylı kullanımının gösterildiği örnek "c# console application" projesi bulunuyor.
