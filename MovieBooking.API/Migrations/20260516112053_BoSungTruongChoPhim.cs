using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class BoSungTruongChoPhim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DangChieu",
                table: "Phims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hot",
                table: "Phims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaNhom",
                table: "Phims",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SapChieu",
                table: "Phims",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DangChieu",
                table: "Phims");

            migrationBuilder.DropColumn(
                name: "Hot",
                table: "Phims");

            migrationBuilder.DropColumn(
                name: "MaNhom",
                table: "Phims");

            migrationBuilder.DropColumn(
                name: "SapChieu",
                table: "Phims");
        }
    }
}
