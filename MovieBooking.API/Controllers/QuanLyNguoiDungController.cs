using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.API.DTOs.Auth;
using MovieBooking.API.DTOs.Common;
using MovieBooking.API.Entities;
using MovieBooking.API.Interfaces;

namespace MovieBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyNguoiDungController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public QuanLyNguoiDungController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        [HttpPost("DangNhap")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> DangNhap([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<LoginResponseDto>.Error("Dữ liệu không hợp lệ"));
            }

            var (success, token, message) = await _authService.LoginAsync(request.TaiKhoan, request.MatKhau);

            if (!success)
            {
                return BadRequest(ApiResponse<LoginResponseDto>.Error(message));
            }

            var response = new LoginResponseDto
            {
                TaiKhoan = request.TaiKhoan,
                AccessToken = token
            };

            return Ok(ApiResponse<LoginResponseDto>.Success(response, message));
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        [HttpPost("DangKy")]
        public async Task<ActionResult<ApiResponse<string>>> DangKy([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<string>.Error("Dữ liệu không hợp lệ"));
            }

            var nguoiDung = _mapper.Map<NguoiDung>(request);
            var (success, message) = await _authService.RegisterAsync(nguoiDung);

            if (!success)
            {
                return BadRequest(ApiResponse<string>.Error(message));
            }

            return Ok(ApiResponse<string>.Success(null, message));
        }
    }
}
