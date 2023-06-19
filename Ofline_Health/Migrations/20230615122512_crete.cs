using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofline_Health.Migrations
{
    /// <inheritdoc />
    public partial class crete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medicine1Dose",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medicine1Name",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medicine2Dose",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medicine2Name",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medicine3Dose",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medicine3Name",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "PrescriptionId",
                table: "Prescriptions",
                newName: "Prescription_Id");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "Prescription_Id",
                table: "Prescriptions",
                newName: "PrescriptionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Prescriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<string>(
                name: "Medicine2Dose",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicine2Name",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicine3Dose",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicine3Name",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
