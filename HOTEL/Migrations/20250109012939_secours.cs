using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL.Migrations
{
    /// <inheritdoc />
    public partial class secours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Chambres_ChambreId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ChambreId",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "ChambreId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ReservationId",
                table: "Chambres",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_ReservationId",
                table: "Chambres",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chambres_Reservations_ReservationId",
                table: "Chambres",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambres_Reservations_ReservationId",
                table: "Chambres");

            migrationBuilder.DropIndex(
                name: "IX_Chambres_ReservationId",
                table: "Chambres");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Chambres");

            migrationBuilder.AlterColumn<string>(
                name: "ChambreId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ChambreId",
                table: "Reservations",
                column: "ChambreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Chambres_ChambreId",
                table: "Reservations",
                column: "ChambreId",
                principalTable: "Chambres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
