using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class seed_Quiz_Hash_QuizRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Quiz",
                columns: new[] { "Id", "Description", "IsDisabled", "Title", "TotalQuestions" },
                values: new object[] { 1, "A collection of general knowledge questions covering various topics", false, "General Knowledge Quiz", 5 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f1986d0-e7e6-4544-ba5d-2a7a2c6fd74d", new DateTime(2025, 5, 15, 10, 35, 25, 760, DateTimeKind.Utc).AddTicks(1390), "AQAAAAIAAYagAAAAECI9bKyu/1EBWNRFLpC3vh3A54+FN0PMYBcE0sk57AmL53ygH2E6ux0eYK/XpaqYfQ==", "d25f83c4-ab65-40e5-8ca0-fc3e49002b74" });

            migrationBuilder.InsertData(
                table: "Hash",
                columns: new[] { "Id", "IsDisabled", "QuizId" },
                values: new object[] { new Guid("a3a7e3e1-48b6-4d9f-b77f-f3c7b6f18c61"), false, 1 });

            migrationBuilder.InsertData(
                table: "QuizRooms",
                columns: new[] { "QuizId", "RoomId", "IsDisabled" },
                values: new object[] { 1, "41fa0ff2-2778-4018-8bca-438f8d3363b0", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hash",
                keyColumn: "Id",
                keyValue: new Guid("a3a7e3e1-48b6-4d9f-b77f-f3c7b6f18c61"));

            migrationBuilder.DeleteData(
                table: "QuizRooms",
                keyColumns: new[] { "QuizId", "RoomId" },
                keyValues: new object[] { 1, "41fa0ff2-2778-4018-8bca-438f8d3363b0" });

            migrationBuilder.DeleteData(
                table: "Quiz",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ee25808-e472-484b-b5e6-bc9e76bd6d5f", new DateTime(2025, 5, 14, 18, 32, 52, 452, DateTimeKind.Utc).AddTicks(4672), "AQAAAAIAAYagAAAAEMjhq15vx0leRDVRBn7hAPhc5s0Omjqwh4WrHx+kRwZAjlENcWbWI+BEnBmswzvTaQ==", "d7459567-155a-47a1-959e-cdbd5170b6a3" });
        }
    }
}
