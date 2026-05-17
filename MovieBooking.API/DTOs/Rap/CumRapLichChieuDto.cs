namespace MovieBooking.API.DTOs.Rap
{
    public class CumRapLichChieuDto
    {
        public string MaCumRap { get; set; } = string.Empty;
        public string TenCumRap { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public List<RapLichChieuDto> DanhSachRap { get; set; } = new();
    }
}
