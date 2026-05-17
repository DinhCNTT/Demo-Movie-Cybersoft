using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.DatVe
{
    public class LichChieuInsert
    {
        [JsonPropertyName("maPhim")]
        public int MaPhim { get; set; }

        [JsonPropertyName("ngayChieuGioChieu")]
        public string NgayChieuGioChieu { get; set; } = string.Empty;

        [JsonPropertyName("maRap")]
        public string MaRap { get; set; } = string.Empty;

        [JsonPropertyName("giaVe")]
        public decimal GiaVe { get; set; }
    }
}
