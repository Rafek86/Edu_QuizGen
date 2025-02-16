using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class teacherMtoMMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019488ef-b9bd-7b5c-adeb-997f2443d22b");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "enrollmentDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gradeLevel",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "hireDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    grade = table.Column<double>(type: "float", nullable: false),
                    credit = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherStudent",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStudent", x => new { x.StudentId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_TeacherStudent_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStudent_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    grade = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentCourse_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCourses",
                columns: table => new
                {
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourses", x => new { x.TeacherId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_TeacherCourses_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseId",
                table: "StudentCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_CourseId",
                table: "TeacherCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudent_TeacherId",
                table: "TeacherStudent",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "TeacherCourses");

            migrationBuilder.DropTable(
                name: "TeacherStudent");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "enrollmentDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "gradeLevel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "hireDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "profilePicture",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019488ef-b9bd-7b5c-adeb-997f2443d22b", 0, "019488f4-5437-7bd0-88a4-491c73dde6ea", "admin@GuizGen.com", true, "QuizGen_Admin", "Team", false, null, "ADMIN@GUIZGEN.COM", "ADMIN@GUIZGEN.COM", "AQAAAAIAAYagAAAAEOtro3sJaSqAzR2f668Vh5b02gWFbirbPU1kIhuI8uw5CXksM9BpfUXrpAjkIdnaeA==", null, false, "GE49A7A12DC0F4459930840D718724172", false, "admin@GuizGen.com" });
        }
    }
}
