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

* **🌐 RESTful API & Swagger:** * Tüm CRUD işlemlerinin modern API standartlarında sunulması.
    * **Swagger UI** ile interaktif dokümantasyon ve kolay endpoint testi.
* **🧠 AI & NLP Destekli Mesaj Yönetimi:**
    * **Toksisite Analizi:** Müşteri mesajları NLP ile taranır ve uygunsuz içerikler otomatik olarak işaretlenir.
    * **AI Smart-Reply:** Gelen mesajlara profesyonel yanıt taslakları oluşturma.
    * **Gastronomy AI:** Malzemelere göre tarif üretimi ve menü danışmanlığı.
* **👨‍🍳 Akıllı Şef & Görev Algoritması:**
    * Şeflerin `TaskCount` değerine göre dinamik **"Müsait / Meşgul"** statüsü yönetimi.
* **✉️ MailKit Entegrasyonu:** * SMTP üzerinden gerçek zamanlı e-posta gönderimi (OTP ve Rezervasyon onayları).
* **🎨 Modern UI/UX Tasarımı:** * Admin yorgunluğunu minimize eden **Mat Pastel** renk paleti ve responsive yapı.

---

## 🧪🧠 Teknik Detaylar
* **Web API:** Verilerin bağımsız bir servis olarak yönetilmesi ve JSON formatında sunulması.
* **Entity Framework Core:** Code-First yaklaşımı ve LINQ sorguları ile veritabanı yönetimi.
* **IHttpClientFactory:** API servislerinin UI katmanında performanslı ve güvenli tüketimi.
* **AI Integration:** Google Gemini / OpenAI API servisleri ile entegre zeka katmanı.

---

## 🛠️ Kullanılan Teknolojiler
* **Framework:** .NET 8.0 MVC & Web API
* **Documentation:** Swagger / OpenAPI 3.0
* **Veritabanı:** MS SQL Server
* **UI:** HTML5, CSS3 (Custom Pastel Theme), JavaScript, Bootstrap 5
* **Kütüphaneler:** EF Core, MailKit, AutoMapper, FluentValidation, Newtonsoft.Json

---

## 📸 Ekran Görüntüleri


### 📊 Dashboard & Analiz
> Restoranın genel performansını, görev dağılımlarını ve AI analizlerini içeren ana panel.

<img width="1598" height="522" alt="image" src="https://github.com/user-attachments/assets/ea401a00-f615-4fa4-acc7-8114b653d7a5" />

---

<img width="1563" height="548" alt="image" src="https://github.com/user-attachments/assets/f607bb77-fd32-4bd2-afa8-4c727adb5595" />

---

<img width="1514" height="522" alt="image" src="https://github.com/user-attachments/assets/72723cf7-99d6-4d8c-9a56-2c28f02e7b3c" />

---

<img width="1566" height="514" alt="image" src="https://github.com/user-attachments/assets/2d2dbfc3-526e-4f72-b2d2-63c6821239bd" />

---

### 🤖 AI Kontrol Paneli
> Mesajların toksisite analizi, duygu durumu ve AI cevap asistanı ekranları.

<img width="1544" height="275" alt="image" src="https://github.com/user-attachments/assets/6d9ec0ba-0700-4908-b9d0-ecc7ff3cc613" />

---

<img width="1574" height="433" alt="image" src="https://github.com/user-attachments/assets/13df9164-bded-421f-af48-3385930d4679" />

---

<img width="1574" height="788" alt="image" src="https://github.com/user-attachments/assets/d7bf8d13-5233-4df0-90ae-fbcec6d5a3d5" />

---

### 🤖 Yummy UI
> Yummy Restaurant

<img width="1734" height="671" alt="image" src="https://github.com/user-attachments/assets/3a1a0883-40c3-4c42-a270-16a2b7ea8ca4" />

---

<img width="938" height="848" alt="image" src="https://github.com/user-attachments/assets/346da434-1ed1-4226-9a1c-abd12f0cac0b" />

---

<img width="1147" height="489" alt="image" src="https://github.com/user-attachments/assets/3696faef-28f0-4181-b1be-a58d2bcbd6ef" />

---

<img width="1463" height="708" alt="image" src="https://github.com/user-attachments/assets/dd11e9f8-2319-4427-9659-94eb21f8d3e7" />

---

<img width="1078" height="805" alt="image" src="https://github.com/user-attachments/assets/c04c0e0f-b04d-4678-8139-5e8eec903f73" />
