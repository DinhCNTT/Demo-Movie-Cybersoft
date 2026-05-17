using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.DatVe
{
    public class DanhSachVeDat
    {
        [JsonPropertyName("maLichChieu")]
        public int MaLichChieu { get; set; }

        [JsonPropertyName("danhSachVe")]
        public List<VeVM> DanhSachVe { get; set; } = new();
    }
}
