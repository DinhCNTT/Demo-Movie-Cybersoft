using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.DatVe
{
    public class ThongTinPhimPhongVeDto
    {
        [JsonPropertyName("maLichChieu")]
        public int MaLichChieu { get; set; }

        [JsonPropertyName("tenCumRap")]
        public string TenCumRap { get; set; } = string.Empty;

        [JsonPropertyName("tenRap")]
        public string TenRap { get; set; } = string.Empty;

        [JsonPropertyName("diaChi")]
        public string DiaChi { get; set; } = string.Empty;

        [JsonPropertyName("tenPhim")]
        public string TenPhim { get; set; } = string.Empty;

        [JsonPropertyName("hinhAnh")]
        public string HinhAnh { get; set; } = string.Empty;

        [JsonPropertyName("ngayChieuGioChieu")]
        public DateTime NgayChieuGioChieu { get; set; }
    }
}
