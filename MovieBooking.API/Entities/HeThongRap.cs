using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("HeThongRap")]
    public class HeThongRap
    {
        [Key]
        [StringLength(50)]
        public string MaHeThongRap { get; set; } = string.Empty;

        [StringLength(255)]
        public string TenHeThongRap { get; set; } = string.Empty;

        [StringLength(255)]
        public string BiDanh { get; set; } = string.Empty;

        [StringLength(500)]
        public string Logo { get; set; } = string.Empty;

        public virtual ICollection<CumRap> CumRaps { get; set; } = new List<CumRap>();
    }
}
