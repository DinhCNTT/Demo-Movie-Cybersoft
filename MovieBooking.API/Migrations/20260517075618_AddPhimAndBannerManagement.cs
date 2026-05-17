using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPhimAndBannerManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Phims",
                table: "Phims");

            migrationBuilder.RenameTable(
                name: "Phims",
                newName: "Phim");

            migrationBuilder.RenameIndex(
                name: "IX_Phims_BiDanh",
                table: "Phim",
                newName: "IX_Phim_BiDanh");

            migrationBuilder.AlterColumn<string>(
                name: "Trailer",
                table: "Phim",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "TenPhim",
                table: "Phim",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "Phim",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HinhAnh",
                table: "Phim",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "BiDanh",
                table: "Phim",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "DangChieu",
                table: "Phim",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hot",
                table: "Phim",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SapChieu",
                table: "Phim",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phim",
                table: "Phim",
                column: "MaPhim");

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    MaBanner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhim = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.MaBanner);
                    table.ForeignKey(
                        name: "FK_Banner_Phim_MaPhim",
                        column: x => x.MaPhim,
                        principalTable: "Phim",
                        principalColumn: "MaPhim",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phim_NgayKhoiChieu",
                table: "Phim",
                column: "NgayKhoiChieu");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_MaPhim",
                table: "Banner",
                column: "MaPhim");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phim",
                table: "Phim");

            migrationBuilder.DropIndex(
                name: "IX_Phim_NgayKhoiChieu",
                table: "Phim");

            migrationBuilder.DropColumn(
                name: "DangChieu",
                table: "Phim");

            migrationBuilder.DropColumn(
                name: "Hot",
                table: "Phim");

            migrationBuilder.DropColumn(
                name: "SapChieu",
                table: "Phim");

            migrationBuilder.RenameTable(
                name: "Phim",
                newName: "Phims");

            migrationBuilder.RenameIndex(
                name: "IX_Phim_BiDanh",
                table: "Phims",
                newName: "IX_Phims_BiDanh");

            migrationBuilder.AlterColumn<string>(
                name: "Trailer",
                table: "Phims",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenPhim",
                table: "Phims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "Phims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HinhAnh",
                table: "Phims",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BiDanh",
                table: "Phims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phims",
                table: "Phims",
                column: "MaPhim");
        }
    }
}
