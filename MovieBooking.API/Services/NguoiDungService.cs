using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieBooking.API.Data;
using MovieBooking.API.DTOs;
using MovieBooking.API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieBooking.API.Services
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public NguoiDungService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<bool> DangKy(NguoiDungVM model)
        {
            var exists = await _context.NguoiDungs.AnyAsync(x => x.TaiKhoan == model.TaiKhoan || x.Email == model.Email);
            if (exists) return false;

            var user = new NguoiDung
            {
                TaiKhoan = model.TaiKhoan,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(model.MatKhau),
                Email = model.Email,
                SoDt = model.SoDt,
                MaNhom = string.IsNullOrEmpty(model.MaNhom) ? "GP01" : model.MaNhom,
                MaLoaiNguoiDung = string.IsNullOrEmpty(model.MaLoaiNguoiDung) ? "KhachHang" : model.MaLoaiNguoiDung,
                HoTen = model.HoTen
            };

            _context.NguoiDungs.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ThongTinNguoiDungResponse?> DangNhap(ThongTinDangNhapVM model)
        {
            // Cho phép đăng nhập bằng cả Tài Khoản hoặc Email
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => 
                x.TaiKhoan == model.TaiKhoan || x.Email == model.TaiKhoan);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.MatKhau, user.MatKhau))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "");
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.TaiKhoan),
                new Claim(ClaimTypes.Name, user.HoTen),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("MaLoaiNguoiDung", user.MaLoaiNguoiDung)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ThongTinNguoiDungResponse
            {
                TaiKhoan = user.TaiKhoan,
                HoTen = user.HoTen,
                Email = user.Email,
                SoDT = user.SoDt,
                MaNhom = user.MaNhom,
                MaLoaiNguoiDung = user.MaLoaiNguoiDung,
                AccessToken = tokenHandler.WriteToken(token)
            };
        }
    }
}
