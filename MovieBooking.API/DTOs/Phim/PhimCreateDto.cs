using System.ComponentModel.DataAnnotations;

namespace MovieBooking.API.DTOs.Phim
{
    public class PhimCreateDto
    {
        [Required(ErrorMessage = "Tên phim không ðý?c ð? tr?ng")]
        [StringLength(200)]
        public string TenPhim { get; set; }

        [StringLength(200)]
        public string? BiDanh { get; set; }

        [StringLength(500)]
        public string? Trailer { get; set; }

        [StringLength(500)]
        public string? HinhAnh { get; set; }

        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Ngày kh?i chi?u không ðý?c ð? tr?ng")]
        public DateTime NgayKhoiChieu { get; set; }

        [Range(0, 10, ErrorMessage = "Ðánh giá ph?i t? 0 ð?n 10")]
        public int DanhGia { get; set; }

        public bool Hot { get; set; }

        public bool DangChieu { get; set; }

        public bool SapChieu { get; set; }
    }
}
