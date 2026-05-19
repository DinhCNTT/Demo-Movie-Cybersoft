using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetupWithAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDung_LoaiNguoiDung_MaLoaiNguoiDung",
                table: "NguoiDung");

            migrationBuilder.InsertData(
                table: "LoaiNguoiDung",
                columns: new[] { "MaLoaiNguoiDung", "TenLoai" },
                values: new object[,]
                {
                    { "KhachHang", "Khách Hàng" },
                    { "QuanTri", "Quản Trị" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phims_BiDanh",
                table: "Phims",
                column: "BiDanh");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_Email",
                table: "NguoiDung",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDung_LoaiNguoiDung_MaLoaiNguoiDung",
                table: "NguoiDung",
                column: "MaLoaiNguoiDung",
                principalTable: "LoaiNguoiDung",
                principalColumn: "MaLoaiNguoiDung",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDung_LoaiNguoiDung_MaLoaiNguoiDung",
                table: "NguoiDung");

            migrationBuilder.DropIndex(
                name: "IX_Phims_BiDanh",
                table: "Phims");

            migrationBuilder.DropIndex(
                name: "IX_NguoiDung_Email",
                table: "NguoiDung");

            migrationBuilder.DeleteData(
                table: "LoaiNguoiDung",
                keyColumn: "MaLoaiNguoiDung",
                keyValue: "KhachHang");

            migrationBuilder.DeleteData(
                table: "LoaiNguoiDung",
                keyColumn: "MaLoaiNguoiDung",
                keyValue: "QuanTri");

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDung_LoaiNguoiDung_MaLoaiNguoiDung",
                table: "NguoiDung",
                column: "MaLoaiNguoiDung",
                principalTable: "LoaiNguoiDung",
                principalColumn: "MaLoaiNguoiDung",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
