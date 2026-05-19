using Microsoft.AspNetCore.Mvc;
using MovieBooking.API.DTOs.Common;
using MovieBooking.API.DTOs.Rap;
using MovieBooking.API.Interfaces;

namespace MovieBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyRapController : ControllerBase
    {
        private readonly IRapService _rapService;

        public QuanLyRapController(IRapService rapService)
        {
            _rapService = rapService;
        }

        [HttpGet("LayThongTinHeThongRap")]
        public async Task<IActionResult> LayThongTinHeThongRap(
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft,
            [FromQuery] string maHeThongRap = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft))
                return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var data = await _rapService.LayThongTinHeThongRapAsync(maHeThongRap);
            return Ok(ApiResponse<List<HeThongRapDto>>.Success(data, "Success"));
        }

        [HttpGet("LayThongTinCumRapTheoHeThong")]
        public async Task<IActionResult> LayThongTinCumRapTheoHeThong(
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft,
            [FromQuery] string maHeThongRap = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft))
                return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var data = await _rapService.LayThongTinCumRapTheoHeThongAsync(maHeThongRap);
            return Ok(ApiResponse<List<HeThongRapCumRapDto>>.Success(data, "Success"));
        }

        [HttpGet("LayThongTinLichChieuHeThongRap")]
        public async Task<IActionResult> LayThongTinLichChieuHeThongRap(
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft,
            [FromQuery] string maHeThongRap = "",
            [FromQuery] string maNhom = "GP01")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft))
                return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var data = await _rapService.LayThongTinLichChieuHeThongRapAsync(maHeThongRap, maNhom);
            return Ok(ApiResponse<List<HeThongRapLichChieuDto>>.Success(data, "Success"));
        }

        [HttpGet("LayThongTinLichChieuPhim")]
        public async Task<IActionResult> LayThongTinLichChieuPhim(
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft,
            [FromQuery(Name = "MaPhim")] int maPhim = 0)
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft))
                return BadRequest(ApiResponse<string>.Error("Thiếu TokenCybersoft"));

            var data = await _rapService.LayThongTinLichChieuPhimAsync(maPhim);
            if (data == null)
                return NotFound(ApiResponse<string>.Error("Không tìm thấy phim", 404));

            return Ok(ApiResponse<ThongTinLichChieuPhimDto>.Success(data, "Success"));
        }
    }
}
