using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class addExplanation_seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hash_Quiz_QuizId",
                table: "Hash");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hash",
                table: "Hash");

            migrationBuilder.RenameTable(
                name: "Hash",
                newName: "Hashes");

            migrationBuilder.RenameIndex(
                name: "IX_Hash_QuizId",
                table: "Hashes",
                newName: "IX_Hashes_QuizId");

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileHash",
                table: "Hashes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hashes",
                table: "Hashes",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Hashes",
                keyColumn: "Id",
                keyValue: new Guid("a3a7e3e1-48b6-4d9f-b77f-f3c7b6f18c61"),
                column: "FileHash",
                value: "a3a7e3e1-48b6-4d9f-b77f-f3c7b6f18c61");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25d0fd10-3c1c-4339-acb7-05a0b5fc10cc", new DateTime(2025, 5, 15, 16, 26, 48, 719, DateTimeKind.Utc).AddTicks(2648), "AQAAAAIAAYagAAAAEP0uBLq7ttC2B93RkaWea0LWUZIhFGuf6Zd5uZ3g3+A8+0t0Ih3V2qxm1YB6dgtDkw==", "12581293-7304-4456-a395-04c004b0fe83" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hashes_Quiz_QuizId",
                table: "Hashes",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hashes_Quiz_QuizId",
                table: "Hashes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hashes",
                table: "Hashes");

            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "FileHash",
                table: "Hashes");

            migrationBuilder.RenameTable(
                name: "Hashes",
                newName: "Hash");

            migrationBuilder.RenameIndex(
                name: "IX_Hashes_QuizId",
                table: "Hash",
                newName: "IX_Hash_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hash",
                table: "Hash",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f1986d0-e7e6-4544-ba5d-2a7a2c6fd74d", new DateTime(2025, 5, 15, 10, 35, 25, 760, DateTimeKind.Utc).AddTicks(1390), "AQAAAAIAAYagAAAAECI9bKyu/1EBWNRFLpC3vh3A54+FN0PMYBcE0sk57AmL53ygH2E6ux0eYK/XpaqYfQ==", "d25f83c4-ab65-40e5-8ca0-fc3e49002b74" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hash_Quiz_QuizId",
                table: "Hash",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
