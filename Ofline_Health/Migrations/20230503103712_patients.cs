using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofline_Health.Migrations
{
    /// <inheritdoc />
    public partial class patients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateofBirth",
                table: "Patient",
                newName: "DateOfBirth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Patient",
                newName: "dateofBirth");
        }
    }
}
