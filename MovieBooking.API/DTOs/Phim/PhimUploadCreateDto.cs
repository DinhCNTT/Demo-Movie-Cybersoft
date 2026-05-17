using System.ComponentModel.DataAnnotations;

namespace MovieBooking.API.DTOs.Phim
{
    /// <summary>
    /// DTO cho vi?c thêm phim v?i upload h?nh ?nh
    /// </summary>
    public class PhimUploadCreateDto
    {
        [Required(ErrorMessage = "Tên phim là b?t bu?c")]
        public string TenPhim { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trailer là b?t bu?c")]
        public string Trailer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mô t? là b?t bu?c")]
        public string MoTa { get; set; } = string.Empty;

        public string MaNhom { get; set; } = "GP01";

        [Required(ErrorMessage = "Ngày kh?i chi?u là b?t bu?c")]
        public DateTime NgayKhoiChieu { get; set; }

        [Range(0, 10, ErrorMessage = "Ðánh giá ph?i t? 0 ð?n 10")]
        public int DanhGia { get; set; }

        public bool Hot { get; set; }

        public bool DangChieu { get; set; }

        public bool SapChieu { get; set; }

        /// <summary>
        /// File h?nh ?nh upload
        /// </summary>
        public IFormFile? File { get; set; }
    }
}
