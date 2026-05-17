namespace MovieBooking.API.DTOs.Rap
{
    public class CumRapDto
    {
        public string MaCumRap { get; set; } = string.Empty;
        public string TenCumRap { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public List<RapPhimDto> DanhSachRap { get; set; } = new();
    }
}
