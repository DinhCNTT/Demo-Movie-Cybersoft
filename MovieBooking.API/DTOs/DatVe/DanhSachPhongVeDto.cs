using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.DatVe
{
    public class DanhSachPhongVeDto
    {
        [JsonPropertyName("thongTinPhim")]
        public ThongTinPhimPhongVeDto ThongTinPhim { get; set; } = new();

        [JsonPropertyName("danhSachGhe")]
        public List<GhePhongVeDto> DanhSachGhe { get; set; } = new();
    }
}
