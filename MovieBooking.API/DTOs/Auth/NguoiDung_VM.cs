using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.Auth
{
    public class NguoiDung_VM
    {
        [JsonPropertyName("taiKhoan")]
        public string TaiKhoan { get; set; } = string.Empty;

        [JsonPropertyName("matKhau")]
        public string MatKhau { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("soDt")]
        public string SoDt { get; set; } = string.Empty;

        [JsonPropertyName("maNhom")]
        public string MaNhom { get; set; } = "GP01";

        [JsonPropertyName("hoTen")]
        public string HoTen { get; set; } = string.Empty;
    }
}
