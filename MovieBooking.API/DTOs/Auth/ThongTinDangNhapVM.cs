using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.Auth
{
    public class ThongTinDangNhapVM
    {
        [JsonPropertyName("taiKhoan")]
        public string TaiKhoan { get; set; } = string.Empty;

        [JsonPropertyName("matKhau")]
        public string MatKhau { get; set; } = string.Empty;
    }
}
