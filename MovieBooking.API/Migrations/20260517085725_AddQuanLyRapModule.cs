using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class AddQuanLyRapModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeThongRap",
                columns: table => new
                {
                    MaHeThongRap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenHeThongRap = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BiDanh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeThongRap", x => x.MaHeThongRap);
                });

            migrationBuilder.CreateTable(
                name: "CumRap",
                columns: table => new
                {
                    MaCumRap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenCumRap = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MaHeThongRap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CumRap", x => x.MaCumRap);
                    table.ForeignKey(
                        name: "FK_CumRap_HeThongRap_MaHeThongRap",
                        column: x => x.MaHeThongRap,
                        principalTable: "HeThongRap",
                        principalColumn: "MaHeThongRap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RapPhim",
                columns: table => new
                {
                    MaRap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenRap = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaCumRap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RapPhim", x => x.MaRap);
                    table.ForeignKey(
                        name: "FK_RapPhim_CumRap_MaCumRap",
                        column: x => x.MaCumRap,
                        principalTable: "CumRap",
                        principalColumn: "MaCumRap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichChieu",
                columns: table => new
                {
                    MaLichChieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhim = table.Column<int>(type: "int", nullable: false),
                    MaRap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayChieuGioChieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiaVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichChieu", x => x.MaLichChieu);
                    table.ForeignKey(
                        name: "FK_LichChieu_Phim_MaPhim",
                        column: x => x.MaPhim,
                        principalTable: "Phim",
                        principalColumn: "MaPhim",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichChieu_RapPhim_MaRap",
                        column: x => x.MaRap,
                        principalTable: "RapPhim",
                        principalColumn: "MaRap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HeThongRap",
                columns: new[] { "MaHeThongRap", "BiDanh", "Logo", "TenHeThongRap" },
                values: new object[,]
                {
                    { "BHDStar", "bhd-star-cineplex", "https://movienew.cybersoft.edu.vn/hinhanh/bhd-star-cineplex.png", "BHD Star Cineplex" },
                    { "CGV", "cgv", "https://movienew.cybersoft.edu.vn/hinhanh/cgv.png", "CGV" }
                });

            migrationBuilder.InsertData(
                table: "CumRap",
                columns: new[] { "MaCumRap", "DiaChi", "MaHeThongRap", "TenCumRap" },
                values: new object[,]
                {
                    { "bhd-star-bitexco", "L3-Bitexco Icon 68, 2 Hai Trieu, Q1", "BHDStar", "BHD Star Bitexco" },
                    { "cgv-su-van-hanh", "Tầng 6 Vạn Hạnh Mall, Quận 10", "CGV", "CGV Sư Vạn Hạnh" }
                });

            migrationBuilder.InsertData(
                table: "RapPhim",
                columns: new[] { "MaRap", "MaCumRap", "TenRap" },
                values: new object[,]
                {
                    { "rap-001", "bhd-star-bitexco", "Rạp 1" },
                    { "rap-002", "cgv-su-van-hanh", "Rạp 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CumRap_MaHeThongRap",
                table: "CumRap",
                column: "MaHeThongRap");

            migrationBuilder.CreateIndex(
                name: "IX_LichChieu_MaPhim",
                table: "LichChieu",
                column: "MaPhim");

            migrationBuilder.CreateIndex(
                name: "IX_LichChieu_MaRap",
                table: "LichChieu",
                column: "MaRap");

            migrationBuilder.CreateIndex(
                name: "IX_RapPhim_MaCumRap",
                table: "RapPhim",
                column: "MaCumRap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LichChieu");

            migrationBuilder.DropTable(
                name: "RapPhim");

            migrationBuilder.DropTable(
                name: "CumRap");

            migrationBuilder.DropTable(
                name: "HeThongRap");
        }
    }
}
