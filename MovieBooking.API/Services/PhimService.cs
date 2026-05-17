using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieBooking.API.Data;
using MovieBooking.API.DTOs.Common;
using MovieBooking.API.DTOs.Phim;
using MovieBooking.API.Entities;
using MovieBooking.API.Interfaces;

namespace MovieBooking.API.Services
{
    public class PhimService : IPhimService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PhimService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PhimDto>> GetAllPhimsAsync()
        {
            var phims = await _context.Phims
                .OrderByDescending(p => p.NgayKhoiChieu)
                .ToListAsync();

            return _mapper.Map<List<PhimDto>>(phims);
        }

        public async Task<PagedResult<PhimDto>> GetPhimsPaginationAsync(int page, int pageSize, string? tenPhim = null)
        {
            var query = _context.Phims.AsQueryable();

            // Filter by name if provided
            if (!string.IsNullOrWhiteSpace(tenPhim))
            {
                query = query.Where(p => p.TenPhim.Contains(tenPhim));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var phims = await query
                .OrderByDescending(p => p.NgayKhoiChieu)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var phimDtos = _mapper.Map<List<PhimDto>>(phims);

            return new PagedResult<PhimDto>
            {
                CurrentPage = page,
                Count = phims.Count,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Items = phimDtos
            };
        }

        public async Task<List<PhimDto>> GetPhimsByDateAsync(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Phims.AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(p => p.NgayKhoiChieu >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(p => p.NgayKhoiChieu <= toDate.Value);
            }

            var phims = await query
                .OrderByDescending(p => p.NgayKhoiChieu)
                .ToListAsync();

            return _mapper.Map<List<PhimDto>>(phims);
        }

        public async Task<PhimDto?> GetPhimByIdAsync(int maPhim)
        {
            var phim = await _context.Phims.FindAsync(maPhim);
            return phim == null ? null : _mapper.Map<PhimDto>(phim);
        }

        public async Task<PhimDto> CreatePhimAsync(PhimCreateDto createDto)
        {
            var phim = _mapper.Map<Phim>(createDto);

            // Auto-generate BiDanh if not provided
            if (string.IsNullOrWhiteSpace(phim.BiDanh))
            {
                phim.BiDanh = GenerateSlug(phim.TenPhim);
            }

            _context.Phims.Add(phim);
            await _context.SaveChangesAsync();

            return _mapper.Map<PhimDto>(phim);
        }

        public async Task<PhimDto?> UpdatePhimAsync(PhimUpdateDto updateDto)
        {
            var phim = await _context.Phims.FindAsync(updateDto.MaPhim);
            if (phim == null) return null;

            _mapper.Map(updateDto, phim);

            // Auto-generate BiDanh if not provided
            if (string.IsNullOrWhiteSpace(phim.BiDanh))
            {
                phim.BiDanh = GenerateSlug(phim.TenPhim);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<PhimDto>(phim);
        }

        public async Task<bool> DeletePhimAsync(int maPhim)
        {
            var phim = await _context.Phims.FindAsync(maPhim);
            if (phim == null) return false;

            _context.Phims.Remove(phim);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<BannerDto>> GetBannersAsync()
        {
            var banners = await _context.Banners
                .Include(b => b.Phim)
                .ToListAsync();

            return _mapper.Map<List<BannerDto>>(banners);
        }

        private string GenerateSlug(string text)
        {
            // Simple slug generation - can be improved with better library
            return text.ToLower()
                .Replace(" ", "-")
                .Replace("ð", "d")
                .Replace("á", "a").Replace("à", "a").Replace("?", "a").Replace("?", "a").Replace("?", "a")
                .Replace("ã", "a").Replace("?", "a").Replace("?", "a").Replace("?", "a").Replace("?", "a").Replace("?", "a")
                .Replace("â", "a").Replace("?", "a").Replace("?", "a").Replace("?", "a").Replace("?", "a").Replace("?", "a")
                .Replace("é", "e").Replace("è", "e").Replace("?", "e").Replace("?", "e").Replace("?", "e")
                .Replace("ê", "e").Replace("?", "e").Replace("?", "e").Replace("?", "e").Replace("?", "e").Replace("?", "e")
                .Replace("í", "i").Replace("?", "i").Replace("?", "i").Replace("?", "i").Replace("?", "i")
                .Replace("ó", "o").Replace("?", "o").Replace("?", "o").Replace("?", "o").Replace("?", "o")
                .Replace("ô", "o").Replace("?", "o").Replace("?", "o").Replace("?", "o").Replace("?", "o").Replace("?", "o")
                .Replace("õ", "o").Replace("?", "o").Replace("?", "o").Replace("?", "o").Replace("?", "o").Replace("?", "o")
                .Replace("ú", "u").Replace("ù", "u").Replace("?", "u").Replace("?", "u").Replace("?", "u")
                .Replace("ý", "u").Replace("?", "u").Replace("?", "u").Replace("?", "u").Replace("?", "u").Replace("?", "u")
                .Replace("?", "y").Replace("?", "y").Replace("?", "y").Replace("?", "y").Replace("?", "y");
        }
    }
}
