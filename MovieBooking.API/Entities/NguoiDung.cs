using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        [StringLength(50)]
        public string TaiKhoan { get; set; } // Khóa chính theo chuẩn Cybersoft

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string SoDt { get; set; }

        [StringLength(20)]
        public string MaNhom { get; set; } // Ví dụ: GP01, GP02

        [StringLength(50)]
        public string MaLoaiNguoiDung { get; set; } // Khóa ngoại
        
        [ForeignKey("MaLoaiNguoiDung")]
        public LoaiNguoiDung LoaiNguoiDung { get; set; } // Điều hướng (Navigation property)

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }
    }
}
