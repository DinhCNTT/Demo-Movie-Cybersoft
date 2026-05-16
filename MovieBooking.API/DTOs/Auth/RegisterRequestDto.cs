using System.ComponentModel.DataAnnotations;

namespace MovieBooking.API.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Tài kho?n không ðý?c ð? tr?ng")]
        [StringLength(50)]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "M?t kh?u không ðý?c ð? tr?ng")]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Email không ðý?c ð? tr?ng")]
        [EmailAddress(ErrorMessage = "Email không ðúng ð?nh d?ng")]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(20)]
        public string SoDt { get; set; }

        [Required(ErrorMessage = "M? nhóm không ðý?c ð? tr?ng")]
        [StringLength(20)]
        public string MaNhom { get; set; }

        [Required(ErrorMessage = "H? tên không ðý?c ð? tr?ng")]
        [StringLength(100)]
        public string HoTen { get; set; }
    }
}
