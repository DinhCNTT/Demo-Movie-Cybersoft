using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("CumRap")]
    public class CumRap
    {
        [Key]
        [StringLength(50)]
        public string MaCumRap { get; set; } = string.Empty;

        [StringLength(255)]
        public string TenCumRap { get; set; } = string.Empty;

        [StringLength(500)]
        public string DiaChi { get; set; } = string.Empty;

        [StringLength(50)]
        public string MaHeThongRap { get; set; } = string.Empty;

        [ForeignKey(nameof(MaHeThongRap))]
        public virtual HeThongRap? HeThongRap { get; set; }

        public virtual ICollection<RapPhim> Raps { get; set; } = new List<RapPhim>();
    }
}
