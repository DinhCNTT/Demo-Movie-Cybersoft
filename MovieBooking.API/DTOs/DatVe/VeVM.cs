using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.DatVe
{
    public class VeVM
    {
        [JsonPropertyName("maGhe")]
        public int MaGhe { get; set; }

        [JsonPropertyName("giaVe")]
        public decimal GiaVe { get; set; }
    }
}
