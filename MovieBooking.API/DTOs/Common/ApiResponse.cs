using System.Text.Json.Serialization;

namespace MovieBooking.API.DTOs.Common
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public T? Content { get; set; }

        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; } = DateTime.Now;

        public static ApiResponse<T> Success(T? data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                StatusCode = 200,
                Message = message,
                Content = data
            };
        }

        public static ApiResponse<T> Error(string message, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Content = default
            };
        }
    }
}
