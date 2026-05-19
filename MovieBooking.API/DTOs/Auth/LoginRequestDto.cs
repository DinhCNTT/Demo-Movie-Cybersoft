using System.ComponentModel.DataAnnotations;

namespace MovieBooking.API.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string MatKhau { get; set; }
    }
}
