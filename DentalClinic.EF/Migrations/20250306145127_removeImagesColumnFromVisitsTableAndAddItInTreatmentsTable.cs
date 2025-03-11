using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.EF.Migrations
{
    /// <inheritdoc />
    public partial class removeImagesColumnFromVisitsTableAndAddItInTreatmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TreatmentId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_TreatmentId",
                table: "Images",
                column: "TreatmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Treatments_TreatmentId",
                table: "Images",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Treatments_TreatmentId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TreatmentId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "Images");
        }
    }
}
