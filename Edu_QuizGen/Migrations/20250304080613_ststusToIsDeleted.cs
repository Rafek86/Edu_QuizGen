using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class ststusToIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Rooms",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Quiz",
                newName: "IsDisabled");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Rooms",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Quiz",
                newName: "Status");
        }
    }
}
