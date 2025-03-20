using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddFinanceIdColumnInVisitsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinanceId",
                table: "Visits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_FinanceId",
                table: "Visits",
                column: "FinanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Finances_FinanceId",
                table: "Visits",
                column: "FinanceId",
                principalTable: "Finances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Finances_FinanceId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_FinanceId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "FinanceId",
                table: "Visits");
        }
    }
}
