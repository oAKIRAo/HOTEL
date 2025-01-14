using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL.Migrations
{
    /// <inheritdoc />
    public partial class addreservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chambres_ReservationId",
                table: "Chambres");

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_ReservationId",
                table: "Chambres",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chambres_ReservationId",
                table: "Chambres");

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_ReservationId",
                table: "Chambres",
                column: "ReservationId",
                unique: true,
                filter: "[ReservationId] IS NOT NULL");
        }
    }
}
