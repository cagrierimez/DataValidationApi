# DataValidationApi/README.md

# 📊 Data Validation & Excel Upload API

Bu proje, kullanıcıların Excel dosyalarını API üzerinden yükleyip doğrulamasını sağlar.
Geçerli kayıtlar veritabanına kaydedilir, geçersiz olanlar ayrı bir Excel çıktısına alınır.

---

## 🚀 Özellikler
- ✅ Excel dosyası yükleme (Swagger UI üzerinden test edilebilir)
- ✅ FluentValidation ile veri doğrulama
- ✅ Geçerli kayıtları DB’ye kaydetme
- ✅ Hatalı kayıtları ayrı bir Excel dosyası olarak indirme
- ✅ Katmanlı mimari (API, Service, DataAccess)

---

## 🛠 Kullanılan Teknolojiler
- [.NET 8](https://dotnet.microsoft.com/)
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/)
- [FluentValidation](https://fluentvalidation.net/)
- [ClosedXML](https://github.com/ClosedXML/ClosedXML) (Excel işlemleri için)
- Swagger (API dokümantasyonu için)

---

## 📚 Proje Mimarisi
```
src/
 ├── API                # Controller & Swagger
 ├── Service            # İş mantığı (Business Layer)
 ├── DataAccess         # Repository & DB işlemleri
 └── Core               # Ortak modeller ve altyapı
```

---

## ⚙️ Kurulum

1. Repo’yu klonla:  
```bash
git clone https://github.com/kullaniciadi/DataValidationApi.git
cd DataValidationApi
```

2. Gerekli bağımlılıkları yükle:  
```bash
dotnet restore
```

3. Uygulamayı çalıştır:  
```bash
dotnet run --project API
```

4. Tarayıcıdan Swagger UI’a git:  
👉 [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)

---

## 📊 Veri Akış Diyagramı

```text
 Excel/CSV File
       ↓
[ ExcelService ]
  - Read rows
  - Map DTO
       ↓
[ Validator (FluentValidation) ]
       ↓
 ┌──────────────┬───────────────┐
 │              │               │
Valid Records   Invalid Records
 │              │
 ↓              ↓
DB (optional)   InvalidRecords.xlsx
```

---

## 🧪 Örnek Kullanım

- Swagger UI’dan `/api/excel/upload` endpointine Excel dosyası yükle  
- Yanıt:  
```json
{
  "validRecords": 120,
  "invalidRecords": 8,
  "errorFile": "InvalidRecords.xlsx"
}
```

 
 
