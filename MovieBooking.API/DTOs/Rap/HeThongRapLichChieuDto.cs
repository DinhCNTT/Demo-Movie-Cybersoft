namespace MovieBooking.API.DTOs.Rap
{
    public class HeThongRapLichChieuDto
    {
        public string MaHeThongRap { get; set; } = string.Empty;
        public string TenHeThongRap { get; set; } = string.Empty;
        public string BiDanh { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public List<CumRapLichChieuDto> lstCumRap { get; set; } = new();
    }
}
