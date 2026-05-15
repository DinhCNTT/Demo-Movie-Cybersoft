using Microsoft.EntityFrameworkCore;
using MovieBooking.API.Entities;

namespace MovieBooking.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Phim> Phims { get; set; }
        public DbSet<LoaiNguoiDung> LoaiNguoiDungs { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
    }
}
