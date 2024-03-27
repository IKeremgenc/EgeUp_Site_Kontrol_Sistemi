# Proje Adı: EgeUp_Site_Kontrol_Sistemi

Bu proje, bir yönetici tarafından referans kodu oluşturup kullanıcılara otomatik olarak mail yoluyla gönderilen bir sistem içerir. Maildeki referans kodunu kullanan kullanıcılar kayıt oluştururken bu kodu kullanarak giriş yapabilirler. Referans kodu, kullanıcının bir şirkete ait olduğunu belirtir. Şirketlerin farklı paket seçenekleri vardır ve her paket belirli bir site eklemesini sağlar. Eklenen site Hangfire sayesinde her dakika kontrol edilir ve eklenen site çöktüğünde siteyi ekleyenlere mail gönderir. Site düzeldiğinde tekrar mail göndererek sitenin düzeldiğini bildirir.

## Özellikler

- **Referans Kodu Oluşturma ve Mail Gönderme:** Admin tarafından oluşturulan referans kodları, kullanıcılara otomatik olarak mail yoluyla gönderilir.
- **Referans Kodu ile Giriş:** Kullanıcılar, kayıt olurken veya giriş yaparken referans kodunu kullanarak sisteme erişebilirler.
- **Kullanıcı Sayısı Kontrolü:** Şirketlerin belirli bir pakete sahip olmaları durumunda toplam kullanıcı sayılarını gösterir.
- **Site Sağlığı İzleme:** Hangfire ile her dakikada bir siteyi pingler. Eğer site 404, 500, 502 gibi hatalar alırsa, kullanıcıya çökme raporu gönderilir. Site düzeldiğinde ise kullanıcıya "Site ayakta" bilgisi verilir.
- **Admin Paneli:** Admin panelinde kullanıcılar görüntülenebilir, işlemler yapılabilir, site ekleyebilir ve şirketin güncel bilgileri görüntülenebilir.
- **Kullanıcı Paneli:** Kullanıcı panelinde şirketteki kullanıcılar görüntülenebilir, eklenen sitelerin durumu kontrol edilebilir, kişisel ayarlar güncellenebilir ve şifre kurtarma epostası alınabilir.
  
## Kullanılan Teknolojiler
- **ASP.NET Core 5.0:** Web uygulaması geliştirmek için kullanılan bir platform.
- **X.PagedList:** Sayfalama işlemleri için kullanılan bir kütüphane.
- **N Katmanlı Mimar:** Modüler ve genişletilebilir bir mimari tasarım deseni.
- **DTO (Data Transfer Object):** Veri transfer nesneleri oluşturmak için kullanılan bir desen.
- **Validator:** Girdi doğrulama işlemleri için kullanılan bir kütüphane.
- **Hangfire:** Görev yöneticisi ve zamanlanmış işler için kullanılan bir araç.
- **Identity ve Entity Framework:** Kullanıcı ve rol yönetimi için kullanılan kimlik doğrulama ve veritabanı ORM kütüphaneleri.
- **Otomatik Migration ve Kullanıcı/Rol Oluşturma:** Veritabanı yönetimi için otomatik işlemler.
- **404 Hatası Sayfası:** Site üzerinde oluşan hataları yönlendirmek için özel bir sayfa.

## Kurulum
1. Projenin kopyasını bilgisayarınıza indirin.
Projeyi çalıştırdığınızda otomatik olarak veritabanında tablolar oluşturulacak ve admin hesabı da otomatik olarak oluşturulacaktır.
3.Oluşan admin hesabı: Adı: EgeUp, Şifre: EgeUp123
4.Admin paneline giriş yaparak kendiniz kullanıma başlayabilirsiniz.


---

Bu README dosyası, EgeUp_Site_Kontrol_Sistemi projesi için geliştirilmiş bir açıklama ve kurulum rehberi içermektedir. Projeyle ilgili daha fazla bilgi için lütfen GitHub deposunu ziyaret edin. Teşekkürler!
