using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.API.DTOs.Common;
using MovieBooking.API.DTOs.DatVe;
using MovieBooking.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace MovieBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyDatVeController : ControllerBase
    {
        private readonly IDatVeService _datVeService;

        public QuanLyDatVeController(IDatVeService datVeService)
        {
            _datVeService = datVeService;
        }

        [Authorize]
        [HttpPost("DatVe")]
        public async Task<IActionResult> DatVe(
            [FromBody] DanhSachVeDat? DanhSachVe,
            [FromHeader(Name = "Authorization")] string authorization,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft)
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft))
                return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            if (DanhSachVe == null)
                return BadRequest(ApiResponse<string>.Error("DanhSachVe không hợp lệ"));

            try
            {
                var taiKhoan = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value
                              ?? User.Identity?.Name
                              ?? string.Empty;

                if (string.IsNullOrWhiteSpace(taiKhoan))
                    return Unauthorized(ApiResponse<string>.Error("Không xác thực được tài khoản", 401));

                await _datVeService.DatVeAsync(DanhSachVe, taiKhoan);
                return Ok(ApiResponse<string>.Success("Đặt vé thành công", "Success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Error(ex.Message));
            }
        }

        [HttpGet("LayDanhSachPhongVe")]
        public async Task<IActionResult> LayDanhSachPhongVe(
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft,
            [FromQuery(Name = "MaLichChieu")] int MaLichChieu = 0)
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft))
                return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var data = await _datVeService.LayDanhSachPhongVeAsync(MaLichChieu);
            if (data == null)
                return NotFound(ApiResponse<string>.Error("Không tìm thấy lịch chiếu", 404));

            return Ok(ApiResponse<DanhSachPhongVeDto>.Success(data, "Success"));
        }

        [Authorize]
        [HttpPost("TaoLichChieu")]
        public async Task<IActionResult> TaoLichChieu(
            [FromBody] LichChieuInsert? lich,
            [FromHeader(Name = "Authorization")] string authorization,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft)
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft))
                return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            if (lich == null)
                return BadRequest(ApiResponse<string>.Error("Dữ liệu lịch chiếu không hợp lệ"));

            try
            {
                var created = await _datVeService.TaoLichChieuAsync(lich);
                return Ok(ApiResponse<object>.Success(new
                {
                    maLichChieu = created.MaLichChieu,
                    maPhim = created.MaPhim,
                    maRap = created.MaRap,
                    ngayChieuGioChieu = created.NgayChieuGioChieu,
                    giaVe = created.GiaVe
                }, "Success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Error(ex.Message));
            }
        }
    }
}
