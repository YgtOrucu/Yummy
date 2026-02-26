# 🍽️✨ Yummy ✨🍽️

M&Y Yazılım Eğitim Akademi Danışmanlık bünyesinde, **Murat Yücedağ** hocamın rehberliğinde tamamlamış olduğum ve hocamızın **Apı Proje Kampı Serisinden** ilham alarak tasarladığım bu proje, .NET 8.0 MVC ve Web API mimarisi üzerine inşa edilmiş; yapay zeka (AI) destekli mesaj analizi, dinamik şef yönetim algoritmaları ve zenginleştirilmiş kullanıcı deneyimi sunan yeni nesil bir restoran yönetim platformudur.

---

## 🎯🚀 Projenin Amacı

Geleneksel restoran yönetimini dijital bir asistanla birleştirerek; şeflerin iş yükünü optimize etmek, müşteri geri bildirimlerini yapay zeka ile analiz etmek (NLP) ve tüm restoran operasyonlarını modern, pastel ve göz yormayan bir arayüz üzerinden yönetmektir.

---

## 🧱⚙️ Mimari Yapı (N-Tier Architecture)

Proje, sürdürülebilir kod ve yüksek ölçeklenebilirlik ilkesiyle katmanlı mimari ve **DTO (Data Transfer Object)** desenleri kullanılarak inşa edilmiştir:

* **📁 Context:** Veritabanı yapılandırmaları ve DbContext yönetimi (Code-First).
* **📁 Controllers:** UI ve API arasındaki veri akışını yöneten kontrol mekanizmaları.
* **📁 Web API:** Verileri dış dünyaya açan ve UI katmanı tarafından tüketilen RESTful servis katmanı.
* **📁 Entities:** Veritabanı nesneleri (`Product`, `Chef`, `Category`, `Reservation`, `Message`).
* **📁 Dtos:** Güvenli veri taşıma nesneleri (Input/Output modelleri).
* **📁 Mapping:** AutoMapper ile nesneler arası hızlı ve güvenli dönüşüm.
* **📁 Validator:** FluentValidation ile sunucu taraflı form doğrulamaları.
* **📁 ViewComponents:** Sayfa içindeki dinamik alanların modüler yönetimi.

---

## 🚀✨ Öne Çıkan Özellikler

* **🌐 RESTful API & Swagger:**
  * Tüm CRUD işlemlerinin modern API standartlarında sunulması.
  * Swagger UI ile interaktif dokümantasyon ve kolay endpoint testi.

* **🧠 AI & NLP Destekli Mesaj Yönetimi:**
  * Toksisite Analizi: Müşteri mesajları NLP ile taranır ve uygunsuz içerikler otomatik olarak işaretlenir.
  * AI Smart-Reply: Gelen mesajlara profesyonel yanıt taslakları oluşturma.
  * Gastronomy AI: Malzemelere göre tarif üretimi ve menü danışmanlığı.

* **👨‍🍳 Akıllı Şef & Görev Algoritması:**
  * Şeflerin `TaskCount` değerine göre dinamik **"Müsait / Meşgul"** statüsü yönetimi.

* **✉️ MailKit Entegrasyonu:**
  * SMTP üzerinden gerçek zamanlı e-posta gönderimi (OTP ve rezervasyon onayları).

* **🎨 Modern UI/UX Tasarımı:**
  * Admin yorgunluğunu minimize eden Mat Pastel renk paleti ve fully responsive yapı.

---

## 🧪🧠 Teknik Detaylar

* **Web API:** Verilerin bağımsız bir servis olarak yönetilmesi ve JSON formatında sunulması.
* **Entity Framework Core:** Code-First yaklaşımı ve LINQ sorguları ile veritabanı yönetimi.
* **IHttpClientFactory:** API servislerinin UI katmanında performanslı ve güvenli tüketimi.
* **AI Integration:** Google Gemini / OpenAI API servisleri ile entegre zeka katmanı.
* **ASP.NET Identity:** Güvenli kimlik doğrulama ve kullanıcı yönetimi altyapısı.

---

## 🔐🛡️ Gelişmiş Kimlik Doğrulama & Güvenlik Mimarisi

Proje, kullanıcı verilerini ve erişim güvenliğini en üst düzeyde tutmak amacıyla tasarlanmış, çok katmanlı ve modern .NET güvenlik mimarisi ile güçlendirilmiştir.

### 📧 Dinamik OTP & Email Doğrulama

* Giriş sırasında email doğrulaması yapılmamış kullanıcılar için otomatik OTP üretimi
* 6 haneli doğrulama kodu ile güvenli hesap aktivasyonu
* `EmailConfirmed` kontrolü ile doğrulanmamış kullanıcı erişiminin engellenmesi
* MailKit entegrasyonu ile gerçek zamanlı SMTP doğrulama sistemi

### 🔑 Güvenli Şifre Sıfırlama Altyapısı

* Identity tabanlı güvenli token üretimi (`GeneratePasswordResetTokenAsync`)
* Manipülasyona kapalı, tek kullanımlık reset token sistemi
* SMTP üzerinden güvenli şifre sıfırlama akışı
* Güvenli ve doğrulanabilir şifre güncelleme süreci

### 🍪 Güvenli Oturum & Claim Yönetimi

* Claim tabanlı kimlik doğrulama mimarisi
* Şifrelenmiş Cookie tabanlı oturum yönetimi
* Kullanıcı kimlik bilgilerinin güvenli ve performanslı taşınması
* Yetkisiz erişim girişimlerine karşı korumalı session yapısı

### 🌐 Güvenli ve Optimize API İletişimi

* `IHttpClientFactory` ile güvenli ve performanslı HTTP iletişimi
* Connection pooling ile performans optimizasyonu
* Memory leak riskini önleyen modern HttpClient yönetimi
* Timeout ve hata toleranslı network yönetimi

### ✅ Sağlanan Güvenlik Kazanımları

* Çok katmanlı kimlik doğrulama sistemi
* OTP tabanlı hesap doğrulama
* Token tabanlı güvenli şifre sıfırlama
* Güvenli oturum ve kimlik yönetimi
* Modern .NET güvenlik standartlarına uygun mimari

---

## 🛠️ Kullanılan Teknolojiler

* **Framework:** .NET 8.0 MVC & Web API
* **Authentication:** ASP.NET Identity
* **Documentation:** Swagger / OpenAPI 3.0
* **Veritabanı:** MS SQL Server
* **UI:** HTML5, CSS3 (Custom Pastel Theme), JavaScript, Bootstrap 5
* **Kütüphaneler:** EF Core, MailKit, AutoMapper, FluentValidation, Newtonsoft.Json

---

## 📸 Ekran Görüntüleri

Bu bölüm, platformun kimlik doğrulama altyapısını, yönetim panelini, AI yeteneklerini ve kullanıcı arayüzünü uçtan uca göstermektedir.

---

## 🔐 Kimlik Doğrulama & Güvenlik Akışı

> ASP.NET Identity altyapısı, OTP doğrulama, güvenli giriş ve şifre sıfırlama süreçleri.

### 📝 Kullanıcı Kayıt & 🔑 Giriş Yap
<p align="center">
  <img width="1000" src="https://github.com/user-attachments/assets/440e4e43-9868-476e-9ded-fade3f6f0318" />
</p>

<p align="center">
  <img width="1000" src="https://github.com/user-attachments/assets/a2333c79-e758-4c9c-897b-b0170b468dbb" />
</p>

---

### 📬 Email Doğrulama Süreci / Doğrulama Maili

<p align="center">
  <img width="1000" src="https://github.com/user-attachments/assets/06593c52-a487-4437-86c0-92df4d379224" />
</p>

<p align="center">
  <img width="1000" src="https://github.com/user-attachments/assets/ad9dca67-75d9-4386-bb23-45ad21be527e" />
</p>

---

### ♻️ Şifre Sıfırlama Süreci / Doğrulama Maili

<p align="center">
  <img width="1000" src="https://github.com/user-attachments/assets/7c4eca39-5a41-4028-8270-9214acbe4f90" />
</p>


<p align="center">
 <img width="1000" src="https://github.com/user-attachments/assets/25ff67d6-b040-470b-af39-5f5d68323556" />
</p>

<p align="center">
 <img width="1000" src="https://github.com/user-attachments/assets/b4a4ad74-a19c-4456-8c28-51c8c5c279a7" />
</p>

---

## 📊 Dashboard & Yönetim Paneli

> Restoran operasyonlarının, görev dağılımlarının ve sistem analizlerinin merkezi kontrol paneli.

<p align="center">
  <img width="1000" src="https://github.com/user-attachments/assets/6fb9da18-f3d0-4636-8910-0f23d5cfcfba" />
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/f607bb77-fd32-4bd2-afa8-4c727adb5595" width="1000"/>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/72723cf7-99d6-4d8c-9a56-2c28f02e7b3c" width="1000"/>
</p>

<p align="center">
  <img width="1000" src="https://github.com/user-attachments/assets/4795b43a-fd1d-4fda-9975-21456ac52582" />
</p>

---

## 🤖 AI Destekli Yönetim & Mesaj Analizi

> NLP tabanlı mesaj analizi, toksisite tespiti ve AI destekli yanıt üretim sistemi.

<p align="center">
 <img width="1000" src="https://github.com/user-attachments/assets/dd73c810-b05d-41e4-8b89-08c2d2519fcd" />
</p>

<p align="center">
 <img width="1000" src="https://github.com/user-attachments/assets/851f56b2-b528-45c5-96f6-bbeb9b1e33b2" />
</p>

<p align="center">
  <img width="1000" src="https://github.com/user-attachments/assets/3dedc4ae-0e44-4822-b8e9-ff3ea204b6e5" />
</p>

---

## 🍽️ Kullanıcı Arayüzü (UI / UX)

> Modern, responsive ve kullanıcı dostu restoran yönetim arayüzü.

<p align="center">
  <img src="https://github.com/user-attachments/assets/3a1a0883-40c3-4c42-a270-16a2b7ea8ca4" width="1000"/>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/346da434-1ed1-4226-9a1c-abd12f0cac0b" width="1000"/>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/3696faef-28f0-4181-b1be-a58d2bcbd6ef" width="1000"/>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/dd11e9f8-2319-4427-9659-94eb21f8d3e7" width="1000"/>
</p>

<p align="center">
 <img width="1000" src="https://github.com/user-attachments/assets/eb4b0e0c-cee2-42dc-85d3-ce07a685c655" />
</p>

---
