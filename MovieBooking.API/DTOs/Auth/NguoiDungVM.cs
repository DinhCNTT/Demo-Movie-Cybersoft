namespace MovieBooking.API.DTOs.Auth
{
    public class NguoiDungVM
    {
        public string TaiKhoan { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SoDt { get; set; } = string.Empty;
        public string MaNhom { get; set; } = "GP01";
        public string MaLoaiNguoiDung { get; set; } = "KhachHang";
        public string HoTen { get; set; } = string.Empty;
    }
}
