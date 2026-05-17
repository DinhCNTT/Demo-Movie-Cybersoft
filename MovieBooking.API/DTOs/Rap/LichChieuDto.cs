namespace MovieBooking.API.DTOs.Rap
{
    public class LichChieuDto
    {
        public int MaLichChieu { get; set; }
        public DateTime NgayChieuGioChieu { get; set; }
        public decimal GiaVe { get; set; }
        public string MaRap { get; set; } = string.Empty;
        public string TenRap { get; set; } = string.Empty;
    }
}
