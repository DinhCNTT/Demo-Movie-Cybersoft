using System;
using System.ComponentModel.DataAnnotations;

namespace MovieBooking.API.Entities
{
    public class Phim
    {
        [Key]
        public int MaPhim { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string TenPhim { get; set; }
        
        [MaxLength(100)]
        public string BiDanh { get; set; }
        
        [MaxLength(500)]
        public string Trailer { get; set; }
        
        [MaxLength(500)]
        public string HinhAnh { get; set; }
        
        public string MoTa { get; set; }
        
        public DateTime NgayKhoiChieu { get; set; }
        
        public int DanhGia { get; set; }
    }
}
