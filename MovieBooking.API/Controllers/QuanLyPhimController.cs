using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.API.DTOs.Common;
using MovieBooking.API.DTOs.Phim;
using MovieBooking.API.Interfaces;

namespace MovieBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyPhimController : ControllerBase
    {
        private readonly IPhimService _phimService;
        private readonly IFileUploadService _fileUploadService;

        public QuanLyPhimController(IPhimService phimService, IFileUploadService fileUploadService)
        {
            _phimService = phimService;
            _fileUploadService = fileUploadService;
        }

        [HttpGet("LayDanhSachBanner")]
        public async Task<IActionResult> LayDanhSachBanner([FromHeader(Name = "TokenCybersoft")] string tokenCybersoft)
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));
            var banners = await _phimService.GetBannersAsync();
            return Ok(ApiResponse<List<BannerDto>>.Success(banners, "L?y danh sách banner thành công"));
        }

        [HttpGet("LayDanhSachPhim")]
        public async Task<IActionResult> LayDanhSachPhim(
            [FromQuery] string maNhom = "GP01",
            [FromQuery] string tenPhim = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));

            var phims = await _phimService.GetAllPhimsAsync();
            var filtered = phims.Where(p => string.IsNullOrWhiteSpace(tenPhim) || p.TenPhim.Contains(tenPhim, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(ApiResponse<List<PhimDto>>.Success(filtered, "L?y danh sách phim thành công"));
        }

        [HttpGet("LayDanhSachPhimPhanTrang")]
        public async Task<IActionResult> LayDanhSachPhimPhanTrang(
            [FromQuery] string maNhom = "GP01",
            [FromQuery] string tenPhim = "",
            [FromQuery] int soTrang = 1,
            [FromQuery] int soPhanTuTrenTrang = 10,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));

            var result = await _phimService.GetPhimsPaginationAsync(soTrang, soPhanTuTrenTrang, tenPhim);
            return Ok(ApiResponse<PagedResult<PhimDto>>.Success(result, "L?y danh sách phim phân trang thành công"));
        }

        [HttpGet("LayDanhSachPhimTheoNgay")]
        public async Task<IActionResult> LayDanhSachPhimTheoNgay(
            [FromQuery] string maNhom = "GP01",
            [FromQuery] string tenPhim = "",
            [FromQuery] int soTrang = 1,
            [FromQuery] int soPhanTuTrenTrang = 10,
            [FromQuery] string tuNgay = "",
            [FromQuery] string denNgay = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));

            DateTime? fromDate = DateTime.TryParse(tuNgay, out var fromValue) ? fromValue : null;
            DateTime? toDate = DateTime.TryParse(denNgay, out var toValue) ? toValue : null;

            var list = await _phimService.GetPhimsByDateAsync(fromDate, toDate);
            if (!string.IsNullOrWhiteSpace(tenPhim))
            {
                list = list.Where(p => p.TenPhim.Contains(tenPhim, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var totalCount = list.Count;
            var items = list.Skip((soTrang - 1) * soPhanTuTrenTrang).Take(soPhanTuTrenTrang).ToList();
            var paged = new PagedResult<PhimDto>
            {
                CurrentPage = soTrang,
                Count = items.Count,
                TotalCount = totalCount,
                TotalPages = soPhanTuTrenTrang <= 0 ? 1 : (int)Math.Ceiling(totalCount / (double)soPhanTuTrenTrang),
                Items = items
            };

            return Ok(ApiResponse<PagedResult<PhimDto>>.Success(paged, "L?y danh sách phim theo ngày thành công"));
        }

        [HttpPost("ThemPhimUploadHinh")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ThemPhimUploadHinh(
            [FromForm] PhimUploadCreateDto frm,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));

            var createDto = new PhimCreateDto
            {
                TenPhim = frm.TenPhim,
                Trailer = frm.Trailer,
                MoTa = frm.MoTa,
                NgayKhoiChieu = frm.NgayKhoiChieu,
                DanhGia = frm.DanhGia,
                Hot = frm.Hot,
                DangChieu = frm.DangChieu,
                SapChieu = frm.SapChieu
            };

            if (frm.File != null)
            {
                createDto.HinhAnh = await _fileUploadService.UploadImageAsync(frm.File, "images/phim");
            }

            var phim = await _phimService.CreatePhimAsync(createDto);
            return Ok(ApiResponse<PhimDto>.Success(phim, "Thêm phim thành công"));
        }

        [Authorize]
        [HttpPost("CapNhatPhimUpload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CapNhatPhimUpload(
            [FromForm] PhimUploadUpdateDto frm,
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));

            var updateDto = new PhimUpdateDto
            {
                MaPhim = frm.MaPhim,
                TenPhim = frm.TenPhim,
                Trailer = frm.Trailer,
                MoTa = frm.MoTa,
                NgayKhoiChieu = frm.NgayKhoiChieu,
                DanhGia = frm.DanhGia,
                Hot = frm.Hot,
                DangChieu = frm.DangChieu,
                SapChieu = frm.SapChieu
            };

            if (frm.File != null)
            {
                updateDto.HinhAnh = await _fileUploadService.UploadImageAsync(frm.File, "images/phim");
            }

            var phim = await _phimService.UpdatePhimAsync(updateDto);
            if (phim == null) return NotFound(ApiResponse<string>.Error("Không t?m th?y phim", 404));

            return Ok(ApiResponse<PhimDto>.Success(phim, "C?p nh?t phim thành công"));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadHinhAnh(
            IFormFile? file,
            [FromQuery] string? tenPhim,
            [FromQuery] string? maNhom,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));
            if (file == null) return BadRequest(ApiResponse<string>.Error("File không h?p l?"));

            var path = await _fileUploadService.UploadImageAsync(file, "images/phim");
            return Ok(path);
        }

        [Authorize]
        [HttpDelete("XP")]
        public async Task<IActionResult> XP(
            [FromQuery(Name = "MaPhim")] int maPhim = 0,
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));
            var deleted = await _phimService.DeletePhimAsync(maPhim);
            if (!deleted) return NotFound(ApiResponse<string>.Error("Không t?m th?y phim", 404));
            return Ok(ApiResponse<string>.Success("XP thành công", "Success"));
        }

        [Authorize]
        [HttpDelete("XoaPhim")]
        public async Task<IActionResult> XoaPhim(
            [FromQuery(Name = "MaPhim")] int maPhim = 0,
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));
            var deleted = await _phimService.DeletePhimAsync(maPhim);
            if (!deleted) return NotFound(ApiResponse<string>.Error("Không t?m th?y phim", 404));
            return Ok(ApiResponse<string>.Success("Xóa phim thành công", "Success"));
        }

        [HttpGet("LayThongTinPhim")]
        public async Task<IActionResult> LayThongTinPhim(
            [FromQuery(Name = "MaPhim")] int maPhim = 0,
            [FromHeader(Name = "TokenCybersoft")] string tokenCybersoft = "")
        {
            if (string.IsNullOrWhiteSpace(tokenCybersoft)) return BadRequest(ApiResponse<string>.Error("Thi?u TokenCybersoft"));
            var phim = await _phimService.GetPhimByIdAsync(maPhim);
            if (phim == null) return NotFound(ApiResponse<string>.Error("Không t?m th?y phim", 404));
            return Ok(ApiResponse<PhimDto>.Success(phim, "Success"));
        }
    }
}
