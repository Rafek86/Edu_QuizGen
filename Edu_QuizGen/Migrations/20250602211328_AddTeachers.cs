using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class AddTeachers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0194895d-f050-7461-b24f-89bf1364a1ba", "11caefd4-1787-43cb-92b1-ec7a68d628c0" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b708974-66a6-413a-91fe-82cc2b28bbc7", new DateTime(2025, 6, 2, 21, 13, 27, 473, DateTimeKind.Utc).AddTicks(6422), "AQAAAAIAAYagAAAAEEHdRqyhHacyit8HhxVE/EUNJK6wKJX9KtaHDK/N7g27ywJn5A9f0g3dEShkRZgxkg==", "bb6d9e07-987f-431c-97b1-51f9eafc3da9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0194895d-f050-7461-b24f-89bf1364a1ba", "11caefd4-1787-43cb-92b1-ec7a68d628c0" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25d0fd10-3c1c-4339-acb7-05a0b5fc10cc", new DateTime(2025, 5, 15, 16, 26, 48, 719, DateTimeKind.Utc).AddTicks(2648), "AQAAAAIAAYagAAAAEP0uBLq7ttC2B93RkaWea0LWUZIhFGuf6Zd5uZ3g3+A8+0t0Ih3V2qxm1YB6dgtDkw==", "12581293-7304-4456-a395-04c004b0fe83" });
        }
    }
}
