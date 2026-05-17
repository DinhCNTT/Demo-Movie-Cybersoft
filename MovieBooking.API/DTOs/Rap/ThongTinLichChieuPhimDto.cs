namespace MovieBooking.API.DTOs.Rap
{
    public class ThongTinLichChieuPhimDto
    {
        public int MaPhim { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public string? BiDanh { get; set; }
        public string? Trailer { get; set; }
        public string? HinhAnh { get; set; }
        public string? MoTa { get; set; }
        public DateTime NgayKhoiChieu { get; set; }
        public int DanhGia { get; set; }
        public List<HeThongRapLichChieuDto> HeThongRapChieu { get; set; } = new();
    }
}
