using MovieBooking.API.DTOs.Common;
using MovieBooking.API.DTOs.Phim;

namespace MovieBooking.API.Interfaces
{
    public interface IPhimService
    {
        Task<List<PhimDto>> GetAllPhimsAsync();
        Task<PagedResult<PhimDto>> GetPhimsPaginationAsync(int page, int pageSize, string? tenPhim = null);
        Task<List<PhimDto>> GetPhimsByDateAsync(DateTime? fromDate, DateTime? toDate);
        Task<PhimDto?> GetPhimByIdAsync(int maPhim);
        Task<PhimDto> CreatePhimAsync(PhimCreateDto createDto);
        Task<PhimDto?> UpdatePhimAsync(PhimUpdateDto updateDto);
        Task<bool> DeletePhimAsync(int maPhim);
        Task<List<BannerDto>> GetBannersAsync();
    }
}
