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
        public DbSet<Banner> Banners { get; set; }
        public DbSet<LoaiNguoiDung> LoaiNguoiDungs { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<HeThongRap> HeThongRaps { get; set; }
        public DbSet<CumRap> CumRaps { get; set; }
        public DbSet<RapPhim> RapPhims { get; set; }
        public DbSet<LichChieu> LichChieus { get; set; }

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

            modelBuilder.Entity<Banner>()
                .HasOne(b => b.Phim)
                .WithMany(p => p.Banners)
                .HasForeignKey(b => b.MaPhim)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CumRap>()
                .HasOne(c => c.HeThongRap)
                .WithMany(h => h.CumRaps)
                .HasForeignKey(c => c.MaHeThongRap)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RapPhim>()
                .HasOne(r => r.CumRap)
                .WithMany(c => c.Raps)
                .HasForeignKey(r => r.MaCumRap)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LichChieu>()
                .HasOne(l => l.Phim)
                .WithMany()
                .HasForeignKey(l => l.MaPhim)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LichChieu>()
                .HasOne(l => l.RapPhim)
                .WithMany(r => r.LichChieus)
                .HasForeignKey(l => l.MaRap)
                .OnDelete(DeleteBehavior.Cascade);

            // Add indexes
            modelBuilder.Entity<NguoiDung>()
                .HasIndex(n => n.Email)
                .IsUnique();

            modelBuilder.Entity<Phim>()
                .HasIndex(p => p.BiDanh);

            modelBuilder.Entity<Phim>()
                .HasIndex(p => p.NgayKhoiChieu);

            modelBuilder.Entity<HeThongRap>().HasData(
                new HeThongRap
                {
                    MaHeThongRap = "BHDStar",
                    TenHeThongRap = "BHD Star Cineplex",
                    BiDanh = "bhd-star-cineplex",
                    Logo = "https://movienew.cybersoft.edu.vn/hinhanh/bhd-star-cineplex.png"
                },
                new HeThongRap
                {
                    MaHeThongRap = "CGV",
                    TenHeThongRap = "CGV",
                    BiDanh = "cgv",
                    Logo = "https://movienew.cybersoft.edu.vn/hinhanh/cgv.png"
                }
            );

            modelBuilder.Entity<CumRap>().HasData(
                new CumRap
                {
                    MaCumRap = "bhd-star-bitexco",
                    TenCumRap = "BHD Star Bitexco",
                    DiaChi = "L3-Bitexco Icon 68, 2 Hai Trieu, Q1",
                    MaHeThongRap = "BHDStar"
                },
                new CumRap
                {
                    MaCumRap = "cgv-su-van-hanh",
                    TenCumRap = "CGV Sý V?n H?nh",
                    DiaChi = "T?ng 6 V?n H?nh Mall, Qu?n 10",
                    MaHeThongRap = "CGV"
                }
            );

            modelBuilder.Entity<RapPhim>().HasData(
                new RapPhim
                {
                    MaRap = "rap-001",
                    TenRap = "R?p 1",
                    MaCumRap = "bhd-star-bitexco"
                },
                new RapPhim
                {
                    MaRap = "rap-002",
                    TenRap = "R?p 2",
                    MaCumRap = "cgv-su-van-hanh"
                }
            );
        }
    }
}

