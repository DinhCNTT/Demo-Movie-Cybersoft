namespace MovieBooking.API.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string TaiKhoan { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDt { get; set; }
        public string MaNhom { get; set; }
        public string MaLoaiNguoiDung { get; set; }
        public string AccessToken { get; set; }
    }
}
