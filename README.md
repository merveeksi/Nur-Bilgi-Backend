# NurBilgi

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

NurBilgi is an Islamic knowledge application that provides various religious services such as daily prayers, duas, Quran surahs, Islamic catechisms, and AI-powered chat support.

</td>
<td>

NurBilgi, günlük namazlar, dualar, Kuran sureleri, dini bilgiler ve yapay zeka destekli sohbet gibi çeşitli dini hizmetler sunan bir İslami bilgi uygulamasıdır.

</td>
</tr>
</table>

## Features | Özellikler

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

- **Prayer Times**: Get accurate prayer times based on location
- **Daily Duas**: Daily supplications with translations
- **Quran Content**: Access to Quran surahs and their meanings
- **Islamic Catechisms**: Questions and answers about Islamic knowledge
- **AI Chat Assistant**: Intelligent chat support for Islamic inquiries
- **User Authentication**: Secure user accounts and profiles
- **Payment Integration**: Premium features through Paddle payment processing
- **Messaging System**: Internal communication system

</td>
<td>

- **Namaz Vakitleri**: Konuma dayalı doğru namaz vakitleri
- **Günlük Dualar**: Tercümeleriyle birlikte günlük dualar
- **Kuran İçeriği**: Kuran surelerine ve anlamlarına erişim
- **İslami Bilgiler**: Dini bilgiler hakkında soru ve cevaplar
- **Yapay Zeka Sohbet Asistanı**: Dini sorular için akıllı sohbet desteği
- **Kullanıcı Kimlik Doğrulama**: Güvenli kullanıcı hesapları ve profilleri
- **Ödeme Entegrasyonu**: Paddle ödeme sistemi ile premium özellikler
- **Mesajlaşma Sistemi**: Dahili iletişim sistemi

</td>
</tr>
</table>

## Project Structure | Proje Yapısı

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

The project follows Clean Architecture principles with the following components:

- **NurBilgi.Domain**: Core business entities, interfaces, and value objects
- **NurBilgi.Application**: Application services, DTOs, and business logic
- **NurBilgi.Infrastructure**: External services implementation, database access
- **NurBilgi.WebApi**: API endpoints and controllers

</td>
<td>

Proje, Clean Architecture prensiplerine uygun olarak aşağıdaki bileşenlerden oluşur:

- **NurBilgi.Domain**: Temel iş varlıkları, arayüzler ve değer nesneleri
- **NurBilgi.Application**: Uygulama servisleri, DTO'lar ve iş mantığı
- **NurBilgi.Infrastructure**: Dış servis uygulamaları, veritabanı erişimi
- **NurBilgi.WebApi**: API uç noktaları ve kontrolcüler

</td>
</tr>
</table>

## Technology Stack | Teknoloji Altyapısı

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

- **.NET 9**: Backend framework
- **Entity Framework Core**: ORM for database access
- **ASP.NET Core Web API**: REST API implementation
- **Identity Framework**: User authentication and authorization
- **Paddle API**: Payment processing
- **AI Integration**: For religious question answering

</td>
<td>

- **.NET 9**: Arka uç çatısı
- **Entity Framework Core**: Veritabanı erişimi için ORM
- **ASP.NET Core Web API**: REST API uygulaması
- **Identity Framework**: Kullanıcı kimlik doğrulama ve yetkilendirme
- **Paddle API**: Ödeme işlemleri
- **Yapay Zeka Entegrasyonu**: Dini soruların yanıtlanması için

</td>
</tr>
</table>

## Getting Started | Başlangıç

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

### Prerequisites

- .NET 9 SDK
- SQL Server or PostgreSQL
- IDE (Visual Studio, VS Code, JetBrains Rider)

</td>
<td>

### Ön Gereksinimler

- .NET 9 SDK
- SQL Server veya PostgreSQL
- IDE (Visual Studio, VS Code, JetBrains Rider)

</td>
</tr>
</table>

### Installation | Kurulum

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

1. Clone the repository
   ```
   git clone https://github.com/yourusername/NurBilgi.git
   ```

2. Set up the database connection in `.env` file

3. Navigate to the WebApi project
   ```
   cd src/NurBilgi.WebApi
   ```

4. Run the database migrations
   ```
   dotnet ef database update
   ```

5. Run the application
   ```
   dotnet run
   ```

</td>
<td>

1. Depoyu klonlayın
   ```
   git clone https://github.com/yourusername/NurBilgi.git
   ```

2. `.env` dosyasında veritabanı bağlantısını ayarlayın

3. WebApi projesine gidin
   ```
   cd src/NurBilgi.WebApi
   ```

4. Veritabanı migrasyonlarını çalıştırın
   ```
   dotnet ef database update
   ```

5. Uygulamayı çalıştırın
   ```
   dotnet run
   ```

</td>
</tr>
</table>

### API Endpoints | API Uç Noktaları

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

The API includes endpoints for:
- User authentication and management
- Prayer times retrieval
- Quran content access
- Daily duas
- AI chat interactions
- Payment processing

</td>
<td>

API aşağıdaki uç noktaları içerir:
- Kullanıcı kimlik doğrulama ve yönetimi
- Namaz vakitleri sorgulama
- Kuran içeriği erişimi
- Günlük dualar
- Yapay zeka sohbet etkileşimleri
- Ödeme işlemleri

</td>
</tr>
</table>

## License | Lisans

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

[Insert your license information here]

</td>
<td>

[Lisans bilgilerinizi buraya ekleyin]

</td>
</tr>
</table>

## Contact | İletişim

<table>
<tr>
<th>English</th>
<th>Türkçe</th>
</tr>
<tr>
<td>

[Insert your contact information here]

</td>
<td>

[İletişim bilgilerinizi buraya ekleyin]

</td>
</tr>
</table> 