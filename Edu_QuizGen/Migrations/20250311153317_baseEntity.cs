using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class baseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Teachers",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Students",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AspNetUsers",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AspNetRoles",
                newName: "IsDisabled");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "TeacherStudent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "TeacherCourse",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "StudentRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "StudentCourse",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "QuizRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "QuizResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Options",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Hash",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "TeacherStudent");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "TeacherCourse");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "StudentRooms");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "StudentCourse");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "QuizRoom");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "QuizResults");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Hash");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Teachers",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Students",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "AspNetUsers",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "AspNetRoles",
                newName: "IsDeleted");
        }
    }
}
