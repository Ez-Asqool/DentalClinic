using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.EF.Migrations
{
    /// <inheritdoc />
    public partial class UlterRelationShipToRoomDoctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_RoomId",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_RoomId",
                table: "Doctors",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_RoomId",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_RoomId",
                table: "Doctors",
                column: "RoomId",
                unique: true);
        }
    }
}
