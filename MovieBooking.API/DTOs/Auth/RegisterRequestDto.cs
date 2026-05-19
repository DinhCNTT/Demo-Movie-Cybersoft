using System.ComponentModel.DataAnnotations;

namespace MovieBooking.API.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [StringLength(50)]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(20)]
        public string SoDt { get; set; }

        [Required(ErrorMessage = "Mã nhóm không được để trống")]
        [StringLength(20)]
        public string MaNhom { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string HoTen { get; set; }
    }
}
