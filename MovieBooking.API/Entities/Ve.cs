using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("Ve")]
    public class Ve
    {
        [Key]
        public int MaVe { get; set; }

        public int MaLichChieu { get; set; }

        public int MaGhe { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaVe { get; set; }

        [StringLength(50)]
        public string TaiKhoanNguoiDat { get; set; } = string.Empty;

        [ForeignKey(nameof(MaLichChieu))]
        public virtual LichChieu? LichChieu { get; set; }

        [ForeignKey(nameof(MaGhe))]
        public virtual Ghe? Ghe { get; set; }
    }
}
