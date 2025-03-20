using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.EF.Migrations
{
    /// <inheritdoc />
    public partial class addOneToOneRelationshipBetweenFinancesAndLabsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinanceId",
                table: "Labs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labs_FinanceId",
                table: "Labs",
                column: "FinanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labs_Finances_FinanceId",
                table: "Labs",
                column: "FinanceId",
                principalTable: "Finances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labs_Finances_FinanceId",
                table: "Labs");

            migrationBuilder.DropIndex(
                name: "IX_Labs_FinanceId",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "FinanceId",
                table: "Labs");
        }
    }
}
