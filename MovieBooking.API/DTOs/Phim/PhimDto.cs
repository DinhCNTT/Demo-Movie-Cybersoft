namespace MovieBooking.API.DTOs.Phim
{
    public class PhimDto
    {
        public int MaPhim { get; set; }
        public string TenPhim { get; set; }
        public string? BiDanh { get; set; }
        public string? Trailer { get; set; }
        public string? HinhAnh { get; set; }
        public string? MoTa { get; set; }
        public DateTime NgayKhoiChieu { get; set; }
        public int DanhGia { get; set; }
        public bool Hot { get; set; }
        public bool DangChieu { get; set; }
        public bool SapChieu { get; set; }
    }
}
