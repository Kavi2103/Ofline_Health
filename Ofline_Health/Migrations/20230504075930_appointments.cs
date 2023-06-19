using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofline_Health.Migrations
{
    /// <inheritdoc />
    public partial class appointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctor_DoctorDocotorId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "Medication",
                table: "Prescriptions",
                newName: "Medicine3Name");

            migrationBuilder.RenameColumn(
                name: "Instructions",
                table: "Prescriptions",
                newName: "Medicine3Dose");

            migrationBuilder.RenameColumn(
                name: "Frequency",
                table: "Prescriptions",
                newName: "Medicine2Name");

            migrationBuilder.RenameColumn(
                name: "Dosage",
                table: "Prescriptions",
                newName: "Medicine2Dose");

            migrationBuilder.RenameColumn(
                name: "DoctorDocotorId",
                table: "Prescriptions",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_DoctorDocotorId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_DoctorId");

            migrationBuilder.AddColumn<string>(
                name: "Medicine1Dose",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicine1Name",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctor_DoctorId",
                table: "Prescriptions",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "DocotorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctor_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medicine1Dose",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medicine1Name",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "Medicine3Name",
                table: "Prescriptions",
                newName: "Medication");

            migrationBuilder.RenameColumn(
                name: "Medicine3Dose",
                table: "Prescriptions",
                newName: "Instructions");

            migrationBuilder.RenameColumn(
                name: "Medicine2Name",
                table: "Prescriptions",
                newName: "Frequency");

            migrationBuilder.RenameColumn(
                name: "Medicine2Dose",
                table: "Prescriptions",
                newName: "Dosage");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Prescriptions",
                newName: "DoctorDocotorId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_DoctorDocotorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctor_DoctorDocotorId",
                table: "Prescriptions",
                column: "DoctorDocotorId",
                principalTable: "Doctor",
                principalColumn: "DocotorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
