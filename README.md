# Scooter Kiralama Sistemi

## Proje Amacı

Bu proje, kullanıcıların harita üzerinden müsait scooterları görüntüleyip kiralayabilmesini, yöneticilerin ise scooterları ve kullanıcıları yönetebilmesini sağlayan bir Windows Forms uygulamasıdır.

Sistem, şehir içi mikromobilite kullanımını simüle etmek amacıyla geliştirilmiştir. Kullanıcılar bakiye yükleyebilir, harita üzerinden scooter seçebilir, kiralama işlemi gerçekleştirebilir ve QR kod doğrulaması yapabilir. Yönetici tarafında ise scooter ve kullanıcı yönetimi gerçekleştirilebilir.

## Sistem Gereksinimleri

* Visual Studio 2026 veya üzeri
* .NET 10 SDK

## Kurulum

Projeyi klonlayın:

```bash
git clone https://github.com/zyr1on/Scooter-Kiralama-Sistemi.git
```

veya (eski commit yüklenmemesi ve daha hızlı inmesi için):

```bash
git clone --depth 1 https://github.com/zyr1on/Scooter-Kiralama-Sistemi.git
```

## Derleme ve Çalıştırma

1. `Scooter Kiralama Sistemi.sln` dosyasını Visual Studio ile açın.
2. Projeyi derleyin veyahut çalıştırın

# Uygulama Akışı

Uygulamanın giriş noktası `Program.cs` dosyasıdır. Program başlatıldığında gerekli servisler, veritabanı bağlantıları ve API bileşenleri hazırlanır, ardından kullanıcı giriş ekranı açılır.

### Formlar

* `LoginForms` : Kullanıcı ve yönetici giriş işlemlerinin gerçekleştirildiği giriş ekranı.
* `MainForm` : Kullanıcıların scooter görüntüleme, kiralama ve hesap işlemlerini gerçekleştirdiği ana ekran.
* `AdminForm` : Yönetici işlemlerinin gerçekleştirildiği yönetim paneli.
* `KiralamaForm` : Seçilen scooter için kiralama sürecinin yönetildiği ekran.
* `BakiyeYuklemeFormu` : Kullanıcı hesabına bakiye yükleme işlemlerinin yapıldığı ekran.

### Diğer Dosyalar

* `Program.cs` : Uygulamanın başlangıç noktası ve servis yapılandırmaları.
* `database.txt` : Veritabanı şeması
* `API.md` : API endpointleri ve kullanım dokümantasyonu.



# API

Proje, uygulama içerisinden erişilebilen dahili bir HTTP API sunmaktadır. API sunucusu `Program.cs` dosyasında başlatılır ve varsayılan olarak belirlenen port üzerinden çalışır.

```csharp
var api = new ApiServer(port: 5050);
var router = new ApiRouter(api);

AuthRoutes.Register(router);
RentalRoutes.Register(router);
ScooterRoutes.Register(router);
UserRoutes.Register(router);

api.Start();
```

API ile ilgili endpointler, istek örnekleri ve kullanım detayları için **[API.md](API.md)** dosyasına bakabilirsiniz.

### API Bileşenleri

* `ApiServer.cs` : HTTP sunucusunun oluşturulması ve isteklerin dinlenmesi.
* `ApiRouter.cs` : Endpoint yönlendirme işlemlerinin yönetilmesi.
* `AuthRoutes.cs` : Kimlik doğrulama işlemleri.
* `UserRoutes.cs` : Kullanıcı işlemleri.
* `ScooterRoutes.cs` : Scooter yönetimi ve sorgulama işlemleri.
* `RentalRoutes.cs` : Kiralama işlemleri.

### Yardımcı Sınıflar

* `DatabaseHelper.cs` : SQLite veritabanı bağlantıları ve sorgu işlemleri.
* `Users.cs` : Kullanıcı yönetimi ve kullanıcı işlemleri.
* `Rentals.cs` : Kiralama süreçlerinin yönetimi.
* `ScooterInfo.cs` : Scooter bilgileri ve durum işlemleri.
* `MapHelper.cs` : Harita ve konum işlemleri.
* `BatteryScooterMarker.cs` : Harita üzerinde scooter işaretçilerinin gösterimi.
* `QRHelper.cs` : QR kod oluşturma ve doğrulama işlemleri.
* `JwtHelper.cs` : JWT oluşturma ve doğrulama işlemleri.



<img width="1440" height="1880" alt="image" src="https://github.com/user-attachments/assets/451b1fab-916f-4c7d-9b42-65949b12ee63" />


