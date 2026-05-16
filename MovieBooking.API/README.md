# 🎬 Movie Booking API

API quản lý đặt vé xem phim

## 🚀 Quick Start

### Prerequisites
- .NET 9 SDK
- SQL Server

### Setup
```sh
# 1. Restore packages
dotnet restore

# 2. Setup JWT secrets
dotnet user-secrets init
dotnet user-secrets set "JwtSettings:SecretKey" "YOUR_SECURE_KEY_HERE"
dotnet user-secrets set "JwtSettings:Issuer" "MovieBookingAPI"
dotnet user-secrets set "JwtSettings:Audience" "MovieBookingClient"

# 3. Update database
dotnet ef database update

# 4. Run
dotnet run
```

### Access Swagger
```
https://localhost:5001
```

## 📊 Current Status

### Phase 1: ✅ Complete
- JWT Authentication
- User Registration/Login
- Clean Architecture
- Swagger UI

### Phase 2-8: 🔜 Coming Soon
See project roadmap for details.

## 🛠️ Tech Stack

- .NET 9
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger/OpenAPI
- AutoMapper
- BCrypt

## 📚 Documentation

Tài liệu chi tiết có trong folder `_Docs_Local` (local only, không commit lên Git).

## 🔐 Security

- User secrets for development
- BCrypt password hashing
- JWT Bearer authentication
- HTTPS enforced

## 📝 License

Educational purposes only.

---

**Built with ❤️ for learning**
