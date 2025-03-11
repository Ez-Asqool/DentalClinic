using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.EF.Migrations
{
    /// <inheritdoc />
    public partial class removeVisitIdColumnInImageTableAndAddTreatmentIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Treatments_TreatmentId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Visits_VisitId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VisitId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "TreatmentId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Treatments_TreatmentId",
                table: "Images",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Treatments_TreatmentId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "TreatmentId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_VisitId",
                table: "Images",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Treatments_TreatmentId",
                table: "Images",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Visits_VisitId",
                table: "Images",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
