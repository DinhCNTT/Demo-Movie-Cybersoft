using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("RapPhim")]
    public class RapPhim
    {
        [Key]
        [StringLength(50)]
        public string MaRap { get; set; } = string.Empty;

        [StringLength(255)]
        public string TenRap { get; set; } = string.Empty;

        [StringLength(50)]
        public string MaCumRap { get; set; } = string.Empty;

        [ForeignKey(nameof(MaCumRap))]
        public virtual CumRap? CumRap { get; set; }

        public virtual ICollection<LichChieu> LichChieus { get; set; } = new List<LichChieu>();
    }
}
