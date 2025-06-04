# 🍕 Pizza Shop Management System

A modern, modular restaurant management system built with **.NET Core 7**, designed to automate and streamline operations in a pizza restaurant. The software features secure authentication, role-based access, real-time order management, billing, reporting, and future-ready capabilities for customer-facing interfaces and payment gateway integration.

---

## 🔧 Technologies Used

- .NET Core 7
- Entity Framework Core
- SQL Server
- RESTful APIs
- JWT Authentication
- Swagger (API Testing)
- Microsoft Identity
- Bootstrap + HTML/CSS
- JavaScript / jQuery

---

---

## ⚙️ Key Features

### ✅ Authentication & Authorization
- JWT-based login for secure API access
- Role-Based Access Control (Admin, Chef, Manager, etc.)

### ✅ Order Management
- Track, update, and dispatch orders in real-time
- View order history and kitchen workflow

### ✅ Menu & Inventory
- Add, edit, delete menu items by category
- Inventory auto-updates based on order activity
- Low stock alerts & supplier integration


### ✅ Reporting & Analytics
- View detailed sales reports
- Analyze customer behavior & product performance

---

## 🚀 Getting Started

### 🔑 Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- SQL Server (LocalDB, SQL Express, or Azure SQL)
- Visual Studio 2022 / VS Code
- Postman or Swagger for API testing

---

### 📥 Installation Steps

```
git clone https://github.com/yourusername/PizzaShopManagement.git
cd PizzaShopManagement
```

### 🛠️ Setup Configuration
Update Database Connection
In appsettings.json:

Edit
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PizzaShopDB;Trusted_Connection=True;"
}
```
Replace with your databse connection and Scaffold 

---
### ▶️ Run the Application
```
dotnet run
```
