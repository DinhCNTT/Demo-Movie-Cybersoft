using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.API.Entities
{
    [Table("Banner")]
    public class Banner
    {
        [Key]
        public int MaBanner { get; set; }

        public int MaPhim { get; set; }

        [Required]
        [StringLength(500)]
        public string HinhAnh { get; set; }

        // Foreign key
        [ForeignKey("MaPhim")]
        public virtual Phim? Phim { get; set; }
    }
}
