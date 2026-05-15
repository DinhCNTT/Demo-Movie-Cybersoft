using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieBooking.API.Data;
using MovieBooking.API.Entities;
using MovieBooking.API.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Controllers & Services
builder.Services.AddControllers();
builder.Services.AddScoped<INguoiDungService, NguoiDungService>();

// 3. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// 4. JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "";
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

// OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// --- Data Seeding ---
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Tự động tạo quyền mặc định nếu Database chưa có
    if (!context.LoaiNguoiDungs.Any())
    {
        context.LoaiNguoiDungs.AddRange(
            new LoaiNguoiDung { MaLoaiNguoiDung = "KhachHang", TenLoai = "Khách Hàng" },
            new LoaiNguoiDung { MaLoaiNguoiDung = "QuanTri", TenLoai = "Quản Trị Viên" }
        );
        context.SaveChanges();
    }

    // Tự động tạo tài khoản Admin mặc định để test
    if (!context.NguoiDungs.Any(u => u.TaiKhoan == "admin"))
    {
        context.NguoiDungs.Add(new NguoiDung
        {
            TaiKhoan = "admin",
            MatKhau = BCrypt.Net.BCrypt.HashPassword("123456"), // Mật khẩu mặc định
            Email = "admin@cybersoft.edu.vn",
            SoDt = "0999999999",
            MaNhom = "GP01",
            MaLoaiNguoiDung = "QuanTri",
            HoTen = "Quản Trị Tối Cao"
        });
        context.SaveChanges();
    }
}

// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // Cho phép FE gọi API

app.UseAuthentication(); // Bật xác thực
app.UseAuthorization();  // Bật phân quyền

app.MapControllers(); // Kích hoạt Controllers

app.Run();
