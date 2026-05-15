using Microsoft.AspNetCore.Mvc;
using MovieBooking.API.DTOs;
using MovieBooking.API.Services;

namespace MovieBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyNguoiDungController : ControllerBase
    {
        private readonly INguoiDungService _nguoiDungService;

        public QuanLyNguoiDungController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }

        [HttpPost("DangNhap")]
        public async Task<IActionResult> DangNhap([FromBody] ThongTinDangNhapVM model)
        {
            var result = await _nguoiDungService.DangNhap(model);
            if (result == null)
            {
                return BadRequest("Tài khoản hoặc mật khẩu không đúng!");
            }
            return Ok(result);
        }

        [HttpPost("DangKy")]
        public async Task<IActionResult> DangKy([FromBody] NguoiDungVM model)
        {
            var result = await _nguoiDungService.DangKy(model);
            if (!result)
            {
                return BadRequest("Tài khoản hoặc Email đã tồn tại!");
            }
            return Ok("Đăng ký thành công!");
        }
    }
}
