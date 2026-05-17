using MovieBooking.API.DTOs.Rap;

namespace MovieBooking.API.Interfaces
{
    public interface IRapService
    {
        Task<List<HeThongRapDto>> LayThongTinHeThongRapAsync(string? maHeThongRap);
        Task<List<HeThongRapCumRapDto>> LayThongTinCumRapTheoHeThongAsync(string? maHeThongRap);
        Task<List<HeThongRapLichChieuDto>> LayThongTinLichChieuHeThongRapAsync(string? maHeThongRap, string? maNhom);
        Task<ThongTinLichChieuPhimDto?> LayThongTinLichChieuPhimAsync(int maPhim);
    }
}
