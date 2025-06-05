using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class mena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "EntollmentDate", "FirstName", "GradeLevel", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "profilePicture" },
                values: new object[] { "11caefd4-1787-43cb-92b1-ec7a68d628c9", 0, "90293fcc-0add-4861-a533-27268bbe6120", "JohnDoee@gmail.com", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "", false, "Doee", false, null, null, null, "AQAAAAIAAYagAAAAEH3xCdYP1imtJFYj9GYcPcgU9UNZ5R2n67vGGDefUSym5KbLVWtS7z1zp6hv0bKSfw==", null, false, "7f917065-3107-417e-b21b-2bf3b28ee69e", false, "JohnDoee@gmail.com", "..." });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d013c662-72b8-448c-8bb4-b307ba274b4b", new DateTime(2025, 6, 5, 22, 22, 23, 433, DateTimeKind.Utc).AddTicks(5950), "AQAAAAIAAYagAAAAEMpq3p63L+tWN+Pk3zHINZmjvEAbeVjZr7qRy+w7NZH78SkRF4M6q5JVxQ+JEU6RuA==", "8fad3392-d2d9-4e12-972d-605c392565d3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c9");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25d0fd10-3c1c-4339-acb7-05a0b5fc10cc", new DateTime(2025, 5, 15, 16, 26, 48, 719, DateTimeKind.Utc).AddTicks(2648), "AQAAAAIAAYagAAAAEP0uBLq7ttC2B93RkaWea0LWUZIhFGuf6Zd5uZ3g3+A8+0t0Ih3V2qxm1YB6dgtDkw==", "12581293-7304-4456-a395-04c004b0fe83" });
        }
    }
}
