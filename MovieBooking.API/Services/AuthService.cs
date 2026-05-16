using Microsoft.IdentityModel.Tokens;
using MovieBooking.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using MovieBooking.API.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieBooking.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Token, string Message)> LoginAsync(string taiKhoan, string matKhau)
        {
            try
            {
                var user = await _context.NguoiDungs
                    .Include(u => u.LoaiNguoiDung)
                    .FirstOrDefaultAsync(u => u.TaiKhoan == taiKhoan);

                if (user == null)
                {
                    return (false, null, "Tài kho?n không t?n t?i");
                }

                // Verify password
                if (!BCrypt.Net.BCrypt.Verify(matKhau, user.MatKhau))
                {
                    return (false, null, "M?t kh?u không ðúng");
                }

                // Generate JWT token
                var token = GenerateJwtToken(user);

                return (true, token, "Ðãng nh?p thành công");
            }
            catch (Exception ex)
            {
                return (false, null, $"L?i ðãng nh?p: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> RegisterAsync(Entities.NguoiDung nguoiDung)
        {
            try
            {
                // Check if user exists
                var existingUser = await _context.NguoiDungs
                    .FirstOrDefaultAsync(u => u.TaiKhoan == nguoiDung.TaiKhoan);

                if (existingUser != null)
                {
                    return (false, "Tài kho?n ð? t?n t?i");
                }

                // Check if email exists
                var existingEmail = await _context.NguoiDungs
                    .FirstOrDefaultAsync(u => u.Email == nguoiDung.Email);

                if (existingEmail != null)
                {
                    return (false, "Email ð? ðý?c s? d?ng");
                }

                // Hash password
                nguoiDung.MatKhau = BCrypt.Net.BCrypt.HashPassword(nguoiDung.MatKhau);

                // Set default role if not provided
                if (string.IsNullOrEmpty(nguoiDung.MaLoaiNguoiDung))
                {
                    nguoiDung.MaLoaiNguoiDung = "KhachHang";
                }

                _context.NguoiDungs.Add(nguoiDung);
                await _context.SaveChangesAsync();

                return (true, "Ðãng k? thành công");
            }
            catch (Exception ex)
            {
                return (false, $"L?i ðãng k?: {ex.Message}");
            }
        }

        private string GenerateJwtToken(Entities.NguoiDung user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.TaiKhoan),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("HoTen", user.HoTen),
                new Claim("MaNhom", user.MaNhom ?? ""),
                new Claim("MaLoaiNguoiDung", user.MaLoaiNguoiDung ?? ""),
                new Claim(ClaimTypes.Role, user.MaLoaiNguoiDung ?? "KhachHang"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
