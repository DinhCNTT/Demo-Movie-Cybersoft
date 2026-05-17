using Microsoft.EntityFrameworkCore;
using MovieBooking.API.Data;
using MovieBooking.API.DTOs.Rap;
using MovieBooking.API.Interfaces;

namespace MovieBooking.API.Services
{
    public class RapService : IRapService
    {
        private readonly ApplicationDbContext _context;

        public RapService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HeThongRapDto>> LayThongTinHeThongRapAsync(string? maHeThongRap)
        {
            var query = _context.HeThongRaps.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(maHeThongRap))
            {
                query = query.Where(x => x.MaHeThongRap == maHeThongRap);
            }

            return await query.Select(x => new HeThongRapDto
            {
                MaHeThongRap = x.MaHeThongRap,
                TenHeThongRap = x.TenHeThongRap,
                BiDanh = x.BiDanh,
                Logo = x.Logo
            }).ToListAsync();
        }

        public async Task<List<HeThongRapCumRapDto>> LayThongTinCumRapTheoHeThongAsync(string? maHeThongRap)
        {
            var query = _context.HeThongRaps
                .AsNoTracking()
                .Include(x => x.CumRaps)
                    .ThenInclude(c => c.Raps)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(maHeThongRap))
            {
                query = query.Where(x => x.MaHeThongRap == maHeThongRap);
            }

            return await query.Select(x => new HeThongRapCumRapDto
            {
                MaHeThongRap = x.MaHeThongRap,
                TenHeThongRap = x.TenHeThongRap,
                BiDanh = x.BiDanh,
                Logo = x.Logo,
                lstCumRap = x.CumRaps.Select(c => new CumRapDto
                {
                    MaCumRap = c.MaCumRap,
                    TenCumRap = c.TenCumRap,
                    DiaChi = c.DiaChi,
                    DanhSachRap = c.Raps.Select(r => new RapPhimDto
                    {
                        MaRap = r.MaRap,
                        TenRap = r.TenRap
                    }).ToList()
                }).ToList()
            }).ToListAsync();
        }

        public async Task<List<HeThongRapLichChieuDto>> LayThongTinLichChieuHeThongRapAsync(string? maHeThongRap, string? maNhom)
        {
            var query = _context.HeThongRaps
                .AsNoTracking()
                .Include(h => h.CumRaps)
                    .ThenInclude(c => c.Raps)
                        .ThenInclude(r => r.LichChieus)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(maHeThongRap))
            {
                query = query.Where(x => x.MaHeThongRap == maHeThongRap);
            }

            return await query.Select(h => new HeThongRapLichChieuDto
            {
                MaHeThongRap = h.MaHeThongRap,
                TenHeThongRap = h.TenHeThongRap,
                BiDanh = h.BiDanh,
                Logo = h.Logo,
                lstCumRap = h.CumRaps.Select(c => new CumRapLichChieuDto
                {
                    MaCumRap = c.MaCumRap,
                    TenCumRap = c.TenCumRap,
                    DiaChi = c.DiaChi,
                    DanhSachRap = c.Raps.Select(r => new RapLichChieuDto
                    {
                        MaRap = r.MaRap,
                        TenRap = r.TenRap,
                        LichChieuPhim = r.LichChieus.Select(l => new LichChieuDto
                        {
                            MaLichChieu = l.MaLichChieu,
                            NgayChieuGioChieu = l.NgayChieuGioChieu,
                            GiaVe = l.GiaVe,
                            MaRap = r.MaRap,
                            TenRap = r.TenRap
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToListAsync();
        }

        public async Task<ThongTinLichChieuPhimDto?> LayThongTinLichChieuPhimAsync(int maPhim)
        {
            var phim = await _context.Phims
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MaPhim == maPhim);

            if (phim == null) return null;

            var heThongRap = await _context.HeThongRaps
                .AsNoTracking()
                .Include(h => h.CumRaps)
                    .ThenInclude(c => c.Raps)
                        .ThenInclude(r => r.LichChieus.Where(l => l.MaPhim == maPhim))
                .Where(h => h.CumRaps.Any(c => c.Raps.Any(r => r.LichChieus.Any(l => l.MaPhim == maPhim))))
                .Select(h => new HeThongRapLichChieuDto
                {
                    MaHeThongRap = h.MaHeThongRap,
                    TenHeThongRap = h.TenHeThongRap,
                    BiDanh = h.BiDanh,
                    Logo = h.Logo,
                    lstCumRap = h.CumRaps.Select(c => new CumRapLichChieuDto
                    {
                        MaCumRap = c.MaCumRap,
                        TenCumRap = c.TenCumRap,
                        DiaChi = c.DiaChi,
                        DanhSachRap = c.Raps
                            .Where(r => r.LichChieus.Any(l => l.MaPhim == maPhim))
                            .Select(r => new RapLichChieuDto
                            {
                                MaRap = r.MaRap,
                                TenRap = r.TenRap,
                                LichChieuPhim = r.LichChieus
                                    .Where(l => l.MaPhim == maPhim)
                                    .Select(l => new LichChieuDto
                                    {
                                        MaLichChieu = l.MaLichChieu,
                                        NgayChieuGioChieu = l.NgayChieuGioChieu,
                                        GiaVe = l.GiaVe,
                                        MaRap = r.MaRap,
                                        TenRap = r.TenRap
                                    }).ToList()
                            }).ToList()
                    }).ToList()
                }).ToListAsync();

            return new ThongTinLichChieuPhimDto
            {
                MaPhim = phim.MaPhim,
                TenPhim = phim.TenPhim,
                BiDanh = phim.BiDanh,
                Trailer = phim.Trailer,
                HinhAnh = phim.HinhAnh,
                MoTa = phim.MoTa,
                NgayKhoiChieu = phim.NgayKhoiChieu,
                DanhGia = phim.DanhGia,
                HeThongRapChieu = heThongRap
            };
        }
    }
}
