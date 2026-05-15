using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class TaoBangNguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiNguoiDung",
                columns: table => new
                {
                    MaLoaiNguoiDung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiNguoiDung", x => x.MaLoaiNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "Phims",
                columns: table => new
                {
                    MaPhim = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BiDanh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Trailer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayKhoiChieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DanhGia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phims", x => x.MaPhim);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    TaiKhoan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SoDt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNhom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaLoaiNguoiDung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.TaiKhoan);
                    table.ForeignKey(
                        name: "FK_NguoiDung_LoaiNguoiDung_MaLoaiNguoiDung",
                        column: x => x.MaLoaiNguoiDung,
                        principalTable: "LoaiNguoiDung",
                        principalColumn: "MaLoaiNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaLoaiNguoiDung",
                table: "NguoiDung",
                column: "MaLoaiNguoiDung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "Phims");

            migrationBuilder.DropTable(
                name: "LoaiNguoiDung");
        }
    }
}
