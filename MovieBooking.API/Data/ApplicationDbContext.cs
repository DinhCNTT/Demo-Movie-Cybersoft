using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for LoaiNguoiDung
            modelBuilder.Entity<LoaiNguoiDung>().HasData(
                new LoaiNguoiDung
                {
                    MaLoaiNguoiDung = "QuanTri",
                    TenLoai = "Qu?n Tr?"
                },
                new LoaiNguoiDung
                {
                    MaLoaiNguoiDung = "KhachHang",
                    TenLoai = "Khách Hàng"
                }
            );

            // Configure relationships
            modelBuilder.Entity<NguoiDung>()
                .HasOne(n => n.LoaiNguoiDung)
                .WithMany()
                .HasForeignKey(n => n.MaLoaiNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);

            // Add indexes
            modelBuilder.Entity<NguoiDung>()
                .HasIndex(n => n.Email)
                .IsUnique();

            modelBuilder.Entity<Phim>()
                .HasIndex(p => p.BiDanh);
        }
    }
}

