using MovieBooking.API.DTOs.DatVe;
using MovieBooking.API.Entities;

namespace MovieBooking.API.Interfaces
{
    public interface IDatVeService
    {
        Task<bool> DatVeAsync(DanhSachVeDat request, string taiKhoan);
        Task<DanhSachPhongVeDto?> LayDanhSachPhongVeAsync(int maLichChieu);
        Task<LichChieu> TaoLichChieuAsync(LichChieuInsert request);
    }
}
