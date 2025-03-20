using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.EF.Migrations
{
    /// <inheritdoc />
    public partial class DropColISReceividAndAddPriceColumnInLabsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISReceived",
                table: "Labs");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Labs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Labs");

            migrationBuilder.AddColumn<int>(
                name: "ISReceived",
                table: "Labs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
