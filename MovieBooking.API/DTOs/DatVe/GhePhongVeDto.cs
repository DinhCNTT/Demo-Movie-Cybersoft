using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.DatVe
{
    public class GhePhongVeDto
    {
        [JsonPropertyName("maGhe")]
        public int MaGhe { get; set; }

        [JsonPropertyName("tenGhe")]
        public string TenGhe { get; set; } = string.Empty;

        [JsonPropertyName("loaiGhe")]
        public string LoaiGhe { get; set; } = string.Empty;

        [JsonPropertyName("giaVe")]
        public decimal GiaVe { get; set; }

        [JsonPropertyName("daDat")]
        public bool DaDat { get; set; }

        [JsonPropertyName("taiKhoanNguoiDat")]
        public string TaiKhoanNguoiDat { get; set; } = string.Empty;
    }
}
