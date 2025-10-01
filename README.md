# DataValidationApi/README.md

# 📊 Data Validation & Excel Upload API

This project allows users to upload Excel files via an API and validate their data.
Valid records are saved to the database, while invalid records are exported to a separate Excel file.

---

## 🚀 Features

* ✅ Excel file upload (can be tested via Swagger UI)
* ✅ Data validation using FluentValidation
* ✅ Save valid records to the database
* ✅ Export invalid records to a separate Excel file
* ✅ Layered architecture (API, Service, DataAccess)

---

## 🛠 Technologies Used

* [.NET 8](https://dotnet.microsoft.com/)
* [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/)
* [FluentValidation](https://fluentvalidation.net/)
* [ClosedXML](https://github.com/ClosedXML/ClosedXML) (for Excel operations)
* Swagger (for API documentation)

---

## 📂 Project Structure

```
src/
 ├── API                # Controllers & Swagger
 ├── Service            # Business logic layer
 ├── DataAccess         # Repository & database operations
 └── Core               # Shared models and infrastructure
```

---

## ⚙️ Setup Instructions

1. Clone the repository:

```bash
git clone https://github.com/username/DataValidationApi.git
cd DataValidationApi
```

2. Restore dependencies:

```bash
dotnet restore
```

3. Run the application:

```bash
dotnet run --project API
```

4. Open Swagger UI in your browser:
   👉 [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)

---

## 📊 Data Flow Diagram

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

## 🧪 Example Usage

* Upload an Excel file to `/api/excel/upload` endpoint via Swagger UI.
* Response example:

```json
{
  "validRecords": 120,
  "invalidRecords": 8,
  "errorFile": "InvalidRecords.xlsx"
}
```

---
 
