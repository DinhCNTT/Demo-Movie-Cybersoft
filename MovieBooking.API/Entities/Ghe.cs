using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("Ghe")]
    public class Ghe
    {
        [Key]
        public int MaGhe { get; set; }

        [StringLength(10)]
        public string TenGhe { get; set; } = string.Empty;

        [StringLength(20)]
        public string LoaiGhe { get; set; } = "Thuong";

        [StringLength(50)]
        public string MaRap { get; set; } = string.Empty;

        [ForeignKey(nameof(MaRap))]
        public virtual RapPhim? RapPhim { get; set; }

        public virtual ICollection<Ve> Ves { get; set; } = new List<Ve>();
    }
}
