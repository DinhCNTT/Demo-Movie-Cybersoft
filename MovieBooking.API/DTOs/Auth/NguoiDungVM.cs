using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.Auth
{
    public class NguoiDungVM
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

        [JsonPropertyName("maLoaiNguoiDung")]
        public string MaLoaiNguoiDung { get; set; } = "KhachHang";

        [JsonPropertyName("hoTen")]
        public string HoTen { get; set; } = string.Empty;
    }
}
