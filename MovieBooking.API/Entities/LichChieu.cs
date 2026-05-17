using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("LichChieu")]
    public class LichChieu
    {
        [Key]
        public int MaLichChieu { get; set; }

        public int MaPhim { get; set; }

        [StringLength(50)]
        public string MaRap { get; set; } = string.Empty;

        public DateTime NgayChieuGioChieu { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaVe { get; set; }

        [ForeignKey(nameof(MaPhim))]
        public virtual Phim? Phim { get; set; }

        [ForeignKey(nameof(MaRap))]
        public virtual RapPhim? RapPhim { get; set; }
    }
}
