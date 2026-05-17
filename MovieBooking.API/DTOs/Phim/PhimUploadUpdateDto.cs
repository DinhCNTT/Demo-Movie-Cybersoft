namespace MovieBooking.API.DTOs.Phim
{
    public class PhimUploadUpdateDto
    {
        public int MaPhim { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public string Trailer { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public string MaNhom { get; set; } = "GP01";
        public DateTime NgayKhoiChieu { get; set; }
        public int DanhGia { get; set; }
        public bool Hot { get; set; }
        public bool DangChieu { get; set; }
        public bool SapChieu { get; set; }
        public IFormFile? File { get; set; }
    }
}