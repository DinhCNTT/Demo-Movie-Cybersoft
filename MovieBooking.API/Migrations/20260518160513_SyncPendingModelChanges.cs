using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class SyncPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CumRap",
                keyColumn: "MaCumRap",
                keyValue: "cgv-su-van-hanh",
                column: "TenCumRap",
                value: "CGV Sý V?n H?nh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CumRap",
                keyColumn: "MaCumRap",
                keyValue: "cgv-su-van-hanh",
                column: "TenCumRap",
                value: "CGV Sư V?n H?nh");
        }
    }
}
