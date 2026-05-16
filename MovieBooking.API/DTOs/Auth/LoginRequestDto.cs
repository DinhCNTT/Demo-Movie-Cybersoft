using System.ComponentModel.DataAnnotations;

namespace MovieBooking.API.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Tài kho?n không ðý?c ð? tr?ng")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "M?t kh?u không ðý?c ð? tr?ng")]
        public string MatKhau { get; set; }
    }
}
