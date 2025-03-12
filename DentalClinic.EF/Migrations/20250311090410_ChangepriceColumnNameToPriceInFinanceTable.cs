using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangepriceColumnNameToPriceInFinanceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Finances",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Finances",
                newName: "price");
        }
    }
}
