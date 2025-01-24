using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0194895d-f050-7461-b24f-89bd825b3846", "01948960-9b3e-7104-bd12-5083222a2c8a", false, false, "Admin", "ADMIN" },
                    { "0194895d-f050-7461-b24f-89be04b4f5ad", "01948960-9b3e-7104-bd12-5084e4273ede", false, false, "Student", "STUDENT" },
                    { "0194895d-f050-7461-b24f-89bf1364a1ba", "01948960-9b3e-7104-bd12-508568b47c8c", false, false, "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019488ef-b9bd-7b5c-adeb-997f2443d22b", 0, "019488f4-5437-7bd0-88a4-491c73dde6ea", "admin@GuizGen.com", true, "QuizGen_Admin", "Team", false, null, "ADMIN@GUIZGEN.COM", "ADMIN@GUIZGEN.COM", "AQAAAAIAAYagAAAAECq5HEmDQPK3OlfsqQWjDJ7iWy09/tUrCN21Un1O4j9OMVILfdlKGtk9+MYN24kSwA==", null, false, "GE49A7A12DC0F4459930840D718724172", false, "admin@GuizGen.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0194895d-f050-7461-b24f-89bd825b3846", "019488ef-b9bd-7b5c-adeb-997f2443d22b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0194895d-f050-7461-b24f-89be04b4f5ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0194895d-f050-7461-b24f-89bf1364a1ba");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0194895d-f050-7461-b24f-89bd825b3846", "019488ef-b9bd-7b5c-adeb-997f2443d22b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0194895d-f050-7461-b24f-89bd825b3846");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019488ef-b9bd-7b5c-adeb-997f2443d22b");
        }
    }
}
