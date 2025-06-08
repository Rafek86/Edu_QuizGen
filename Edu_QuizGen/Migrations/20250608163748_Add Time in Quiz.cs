using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeinQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Quiz",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndAt",
                table: "Quiz",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "Quiz",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Quiz",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Duration", "EndAt", "StartAt" },
                values: new object[] { null, new DateTime(2025, 6, 8, 16, 37, 47, 195, DateTimeKind.Utc).AddTicks(8291), new DateTime(2025, 6, 8, 16, 37, 47, 195, DateTimeKind.Utc).AddTicks(8287) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4783209-c76c-4533-82c1-8dec3a463acd", "AQAAAAIAAYagAAAAEBo/edU+aAGblzWQcazk/KFvTwjOYYjLdN2uxdY4ihSk4TR6lkkZo/CtAGTl4VZdyw==", "ac5df42f-cf03-4a44-a15c-dfdf1eee5b62" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47b91eaa-bfca-49aa-83de-3577a6f64a28", new DateTime(2025, 6, 8, 16, 37, 47, 413, DateTimeKind.Utc).AddTicks(843), "AQAAAAIAAYagAAAAENYIiGMdqYxRb2tZrkqiOg+jO/i4oWE/lqaXre0Ob2NItlrh12+vNNcyjH+VSUBqQg==", "9ccf345e-6007-45f2-a51a-eaa2c25488e7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "EndAt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Quiz");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09d96fb4-d0bf-438a-ba42-139acd2a4c58", "AQAAAAIAAYagAAAAEHUnJVYuVCL0IDiuCw9m51L6UZzDEhwZ6JbQyAALWCfsr8RjB+KQHvWd0TsUo6v3mg==", "e4b9ca04-64aa-418d-8eb9-c9213f4f94a9" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fc3ad22-759c-4986-8c65-0aea213b3ecc", new DateTime(2025, 6, 7, 3, 4, 21, 824, DateTimeKind.Utc).AddTicks(8131), "AQAAAAIAAYagAAAAEMXK7ZxHeMDnv+4DKX4LdwBVd6fublgaHHcn7wCXa6OY914H6B5r+dDPJP8zSxTDmQ==", "405c9140-7d82-4615-988d-23fbdf0a07e6" });
        }
    }
}
