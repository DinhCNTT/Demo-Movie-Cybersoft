using Microsoft.EntityFrameworkCore;
using MovieBooking.API.Data;
using MovieBooking.API.DTOs.DatVe;
using MovieBooking.API.Entities;
using MovieBooking.API.Interfaces;
using System.Globalization;

namespace MovieBooking.API.Services
{
    public class DatVeService : IDatVeService
    {
        private readonly ApplicationDbContext _context;

        public DatVeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DatVeAsync(DanhSachVeDat request, string taiKhoan)
        {
            var lichChieu = await _context.LichChieus.FirstOrDefaultAsync(x => x.MaLichChieu == request.MaLichChieu);
            if (lichChieu == null) throw new KeyNotFoundException("Không t?m th?y l?ch chi?u");

            if (request.DanhSachVe == null || request.DanhSachVe.Count == 0)
                throw new InvalidOperationException("Danh sách vé r?ng");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            var maGheList = request.DanhSachVe.Select(x => x.MaGhe).Distinct().ToList();

            var gheHopLe = await _context.Ghes
                .Where(g => maGheList.Contains(g.MaGhe) && g.MaRap == lichChieu.MaRap)
                .Select(g => g.MaGhe)
                .ToListAsync();

            if (gheHopLe.Count != maGheList.Count)
                throw new InvalidOperationException("Có gh? không thu?c r?p c?a l?ch chi?u");

            var gheDaDat = await _context.Ves
                .Where(v => v.MaLichChieu == request.MaLichChieu && maGheList.Contains(v.MaGhe))
                .Select(v => v.MaGhe)
                .ToListAsync();

            if (gheDaDat.Count > 0)
                throw new InvalidOperationException("M?t s? gh? ð? ðý?c ð?t");

            var veEntities = request.DanhSachVe.Select(v => new Ve
            {
                MaLichChieu = request.MaLichChieu,
                MaGhe = v.MaGhe,
                GiaVe = v.GiaVe <= 0 ? lichChieu.GiaVe : v.GiaVe,
                TaiKhoanNguoiDat = taiKhoan
            }).ToList();

            _context.Ves.AddRange(veEntities);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }

        public async Task<DanhSachPhongVeDto?> LayDanhSachPhongVeAsync(int maLichChieu)
        {
            var lichChieu = await _context.LichChieus
                .AsNoTracking()
                .Include(l => l.Phim)
                .Include(l => l.RapPhim)
                    .ThenInclude(r => r!.CumRap)
                .FirstOrDefaultAsync(x => x.MaLichChieu == maLichChieu);

            if (lichChieu == null || lichChieu.RapPhim == null || lichChieu.Phim == null || lichChieu.RapPhim.CumRap == null)
                return null;

            var danhSachGhe = await _context.Ghes
                .AsNoTracking()
                .Where(g => g.MaRap == lichChieu.MaRap)
                .ToListAsync();

            var veDaDat = await _context.Ves
                .AsNoTracking()
                .Where(v => v.MaLichChieu == maLichChieu)
                .ToListAsync();

            var result = new DanhSachPhongVeDto
            {
                ThongTinPhim = new ThongTinPhimPhongVeDto
                {
                    MaLichChieu = lichChieu.MaLichChieu,
                    TenCumRap = lichChieu.RapPhim.CumRap.TenCumRap,
                    TenRap = lichChieu.RapPhim.TenRap,
                    DiaChi = lichChieu.RapPhim.CumRap.DiaChi,
                    TenPhim = lichChieu.Phim.TenPhim,
                    HinhAnh = lichChieu.Phim.HinhAnh ?? string.Empty,
                    NgayChieuGioChieu = lichChieu.NgayChieuGioChieu
                },
                DanhSachGhe = danhSachGhe.Select(g =>
                {
                    var daDat = veDaDat.FirstOrDefault(v => v.MaGhe == g.MaGhe);
                    return new GhePhongVeDto
                    {
                        MaGhe = g.MaGhe,
                        TenGhe = g.TenGhe,
                        LoaiGhe = g.LoaiGhe,
                        GiaVe = lichChieu.GiaVe,
                        DaDat = daDat != null,
                        TaiKhoanNguoiDat = daDat?.TaiKhoanNguoiDat ?? string.Empty
                    };
                }).ToList()
            };

            return result;
        }

        public async Task<LichChieu> TaoLichChieuAsync(LichChieuInsert request)
        {
            var phimTonTai = await _context.Phims.AnyAsync(x => x.MaPhim == request.MaPhim);
            if (!phimTonTai) throw new KeyNotFoundException("Không t?m th?y phim");

            var rapTonTai = await _context.RapPhims.AnyAsync(x => x.MaRap == request.MaRap);
            if (!rapTonTai) throw new KeyNotFoundException("Không t?m th?y r?p");

            var parsed = ParseDateTime(request.NgayChieuGioChieu);

            var lich = new LichChieu
            {
                MaPhim = request.MaPhim,
                MaRap = request.MaRap,
                NgayChieuGioChieu = parsed,
                GiaVe = request.GiaVe
            };

            _context.LichChieus.Add(lich);
            await _context.SaveChangesAsync();

            return lich;
        }

        private static DateTime ParseDateTime(string input)
        {
            var formats = new[]
            {
                "dd/MM/yyyy HH:mm:ss",
                "dd/MM/yyyy HH:mm",
                "MM/dd/yyyy HH:mm:ss",
                "MM/dd/yyyy HH:mm",
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd HH:mm"
            };

            if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var value))
                return value;

            if (DateTime.TryParse(input, out value))
                return value;

            throw new InvalidOperationException("NgayChieuGioChieu không ðúng ð?nh d?ng");
        }
    }
}
