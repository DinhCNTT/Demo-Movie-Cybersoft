using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBooking.API.Data;
using MovieBooking.API.DTOs.Auth;
using MovieBooking.API.DTOs.Common;
using MovieBooking.API.Entities;
using MovieBooking.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace MovieBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyNguoiDungController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;

        public QuanLyNguoiDungController(IAuthService authService, ApplicationDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpGet("LayDanhSachLoaiNguoiDung")]
        public async Task<IActionResult> LayDanhSachLoaiNguoiDung([FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));
            var data = await _context.LoaiNguoiDungs.ToListAsync();
            return Ok(ApiResponse<List<LoaiNguoiDung>>.Success(data, "Success"));
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        [HttpPost("DangNhap")]
        public async Task<IActionResult> DangNhap(
            [FromBody] ThongTinDangNhapVM ndDN,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var (success, token, message) = await _authService.LoginAsync(ndDN.TaiKhoan, ndDN.MatKhau);
            if (!success)
            {
                return BadRequest(ApiResponse<string>.Error(message));
            }

            var user = await _context.NguoiDungs.AsNoTracking().FirstOrDefaultAsync(x => x.TaiKhoan == ndDN.TaiKhoan);
            var content = new
            {
                taiKhoan = user?.TaiKhoan,
                hoTen = user?.HoTen,
                email = user?.Email,
                soDT = user?.SoDt,
                maNhom = user?.MaNhom,
                maLoaiNguoiDung = user?.MaLoaiNguoiDung,
                accessToken = token
            };

            return Ok(ApiResponse<object>.Success(content, "Success"));
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        [HttpPost("DangKy")]
        public async Task<IActionResult> DangKy(
            [FromBody] NguoiDung_VM nd,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var user = new NguoiDung
            {
                TaiKhoan = nd.TaiKhoan,
                MatKhau = nd.MatKhau,
                Email = nd.Email,
                SoDt = nd.SoDt,
                MaNhom = nd.MaNhom,
                HoTen = nd.HoTen,
                MaLoaiNguoiDung = "KhachHang"
            };

            var (success, message) = await _authService.RegisterAsync(user);
            if (!success) return BadRequest(ApiResponse<string>.Error(message));

            return Ok(ApiResponse<string>.Success("Đăng ký thành công", "Success"));
        }

        [HttpGet("LayDanhSachNguoiDung")]
        public async Task<IActionResult> LayDanhSachNguoiDung(
            [FromQuery] string MaNhom = "GP01",
            [FromQuery] string tuKhoa = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var query = _context.NguoiDungs.AsNoTracking().AsQueryable();
            query = query.Where(x => x.MaNhom == MaNhom);
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query = query.Where(x => x.TaiKhoan.Contains(tuKhoa) || x.HoTen.Contains(tuKhoa));
            }

            var data = await query.ToListAsync();
            return Ok(ApiResponse<List<NguoiDung>>.Success(data, "Success"));
        }

        [HttpGet("LayDanhSachNguoiDungPhanTrang")]
        public async Task<IActionResult> LayDanhSachNguoiDungPhanTrang(
            [FromQuery] string MaNhom = "GP01",
            [FromQuery] string tuKhoa = "",
            [FromQuery] int soTrang = 1,
            [FromQuery] int soPhanTuTrenTrang = 20,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var query = _context.NguoiDungs.AsNoTracking().AsQueryable().Where(x => x.MaNhom == MaNhom);
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query = query.Where(x => x.TaiKhoan.Contains(tuKhoa) || x.HoTen.Contains(tuKhoa));
            }

            var totalCount = await query.CountAsync();
            var items = await query.Skip((soTrang - 1) * soPhanTuTrenTrang).Take(soPhanTuTrenTrang).ToListAsync();
            var result = new PagedResult<NguoiDung>
            {
                CurrentPage = soTrang,
                Count = items.Count,
                TotalCount = totalCount,
                TotalPages = soPhanTuTrenTrang <= 0 ? 1 : (int)Math.Ceiling(totalCount / (double)soPhanTuTrenTrang),
                Items = items
            };

            return Ok(ApiResponse<PagedResult<NguoiDung>>.Success(result, "Success"));
        }

        [HttpGet("TimKiemNguoiDung")]
        public Task<IActionResult> TimKiemNguoiDung(
            [FromQuery] string MaNhom = "GP01",
            [FromQuery] string tuKhoa = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
            => LayDanhSachNguoiDung(MaNhom, tuKhoa, tokenCybersoft);

        [HttpGet("TimKiemNguoiDungPhanTrang")]
        public Task<IActionResult> TimKiemNguoiDungPhanTrang(
            [FromQuery] string MaNhom = "GP01",
            [FromQuery] string tuKhoa = "",
            [FromQuery] int soTrang = 1,
            [FromQuery] int soPhanTuTrenTrang = 1,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
            => LayDanhSachNguoiDungPhanTrang(MaNhom, tuKhoa, soTrang, soPhanTuTrenTrang, tokenCybersoft);

        [Authorize]
        [HttpPost("ThongTinTaiKhoan")]
        public async Task<IActionResult> ThongTinTaiKhoan(
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var taiKhoan = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrWhiteSpace(taiKhoan)) return Unauthorized(ApiResponse<string>.Error("Không xác thực được tài khoản", 401));

            var user = await _context.NguoiDungs.AsNoTracking().FirstOrDefaultAsync(x => x.TaiKhoan == taiKhoan);
            if (user == null) return NotFound(ApiResponse<string>.Error("Không tìm thấy người dùng", 404));

            return Ok(ApiResponse<NguoiDung>.Success(user, "Success"));
        }

        [Authorize]
        [HttpPost("LayThongTinNguoiDung")]
        public async Task<IActionResult> LayThongTinNguoiDung(
            [FromQuery] string taiKhoan = "",
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));
            if (string.IsNullOrWhiteSpace(taiKhoan)) return BadRequest(ApiResponse<string>.Error("Thiếu tài khoản"));

            var user = await _context.NguoiDungs.AsNoTracking().FirstOrDefaultAsync(x => x.TaiKhoan == taiKhoan);
            if (user == null) return NotFound(ApiResponse<string>.Error("Không tìm thấy người dùng", 404));

            return Ok(ApiResponse<NguoiDung>.Success(user, "Success"));
        }

        [Authorize(Roles = "QuanTri")]
        [HttpPost("ThemNguoiDung")]
        public async Task<IActionResult> ThemNguoiDung(
            [FromBody] NguoiDungVM nd,
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var user = new NguoiDung
            {
                TaiKhoan = nd.TaiKhoan,
                MatKhau = nd.MatKhau,
                Email = nd.Email,
                SoDt = nd.SoDt,
                MaNhom = nd.MaNhom,
                HoTen = nd.HoTen,
                MaLoaiNguoiDung = string.IsNullOrWhiteSpace(nd.MaLoaiNguoiDung) ? "KhachHang" : nd.MaLoaiNguoiDung
            };

            var (success, message) = await _authService.RegisterAsync(user);
            if (!success) return BadRequest(ApiResponse<string>.Error(message));

            return Ok(ApiResponse<string>.Success("Thêm người dùng thành công", "Success"));
        }

        [Authorize]
        [HttpPut("CapNhatThongTinNguoiDung")]
        public async Task<IActionResult> CapNhatThongTinNguoiDung(
            [FromBody] NguoiDungVM nd,
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.TaiKhoan == nd.TaiKhoan);
            if (user == null) return NotFound(ApiResponse<string>.Error("Không tìm thấy người dùng", 404));

            user.Email = nd.Email;
            user.SoDt = nd.SoDt;
            user.MaNhom = nd.MaNhom;
            user.HoTen = nd.HoTen;
            user.MaLoaiNguoiDung = nd.MaLoaiNguoiDung;

            if (!string.IsNullOrWhiteSpace(nd.MatKhau))
            {
                user.MatKhau = BCrypt.Net.BCrypt.HashPassword(nd.MatKhau);
            }

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<string>.Success("Cập nhật thành công", "Success"));
        }

        [Authorize]
        [HttpPost("CapNhatThongTinNguoiDung")]
        public Task<IActionResult> CapNhat(
            [FromBody] NguoiDungVM nd,
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
            => CapNhatThongTinNguoiDung(nd, authorization, tokenCybersoft);

        [Authorize(Roles = "QuanTri")]
        [HttpDelete("XoaNguoiDung")]
        public async Task<IActionResult> XoaNguoiDung(
            [FromQuery(Name = "TaiKhoan")] string taiKhoan = "",
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));
            if (string.IsNullOrWhiteSpace(taiKhoan)) return BadRequest(ApiResponse<string>.Error("Thiếu tài khoản"));

            var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.TaiKhoan == taiKhoan);
            if (user == null) return NotFound(ApiResponse<string>.Error("Không tìm thấy người dùng", 404));

            _context.NguoiDungs.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<string>.Success("Xóa người dùng thành công", "Success"));
        }
    }
}
