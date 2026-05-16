namespace MovieBooking.API.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Token, string Message)> LoginAsync(string taiKhoan, string matKhau);
        Task<(bool Success, string Message)> RegisterAsync(Entities.NguoiDung nguoiDung);
    }
}
