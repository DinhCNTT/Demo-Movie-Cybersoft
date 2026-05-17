namespace MovieBooking.API.DTOs.Rap
{
    public class RapLichChieuDto
    {
        public string MaRap { get; set; } = string.Empty;
        public string TenRap { get; set; } = string.Empty;
        public List<LichChieuDto> LichChieuPhim { get; set; } = new();
    }
}
