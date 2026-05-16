using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("LoaiNguoiDung")]
    public class LoaiNguoiDung
    {
        [Key]
        [StringLength(50)]
        public string MaLoaiNguoiDung { get; set; } // Khóa chính (KhachHang, QuanTri,...)

        [Required]
        [StringLength(100)]
        public string TenLoai { get; set; }
    }
}
