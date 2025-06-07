using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class quizQuestionRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionDTO",
                columns: table => new
                {
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QuistionId = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => new { x.QuizId, x.QuistionId });
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Questions_QuistionId",
                        column: x => x.QuistionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuistionId",
                table: "QuizQuestions",
                column: "QuistionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionDTO");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90293fcc-0add-4861-a533-27268bbe6120", "AQAAAAIAAYagAAAAEH3xCdYP1imtJFYj9GYcPcgU9UNZ5R2n67vGGDefUSym5KbLVWtS7z1zp6hv0bKSfw==", "7f917065-3107-417e-b21b-2bf3b28ee69e" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b708974-66a6-413a-91fe-82cc2b28bbc7", new DateTime(2025, 5, 15, 16, 26, 48, 719, DateTimeKind.Utc).AddTicks(2648), "AQAAAAIAAYagAAAAEP0uBLq7ttC2B93RkaWea0LWUZIhFGuf6Zd5uZ3g3+A8+0t0Ih3V2qxm1YB6dgtDkw==", "12581293-7304-4456-a395-04c004b0fe83" });
        }
    }
}
