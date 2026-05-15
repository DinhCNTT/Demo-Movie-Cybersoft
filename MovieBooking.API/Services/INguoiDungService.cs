using MovieBooking.API.DTOs;

namespace MovieBooking.API.Services
{
    public interface INguoiDungService
    {
        Task<ThongTinNguoiDungResponse?> DangNhap(ThongTinDangNhapVM model);
        Task<bool> DangKy(NguoiDungVM model);
    }
}
