using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizRoom_Quiz_QuizId",
                table: "QuizRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizRoom_Rooms_RoomId",
                table: "QuizRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizRoom",
                table: "QuizRoom");

            migrationBuilder.RenameTable(
                name: "QuizRoom",
                newName: "QuizRooms");

            migrationBuilder.RenameIndex(
                name: "IX_QuizRoom_RoomId",
                table: "QuizRooms",
                newName: "IX_QuizRooms_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizRooms",
                table: "QuizRooms",
                columns: new[] { "QuizId", "RoomId" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HireDate", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "profilePicture" },
                values: new object[] { "11caefd4-1787-43cb-92b1-ec7a68d628c0", 0, "1ee25808-e472-484b-b5e6-bc9e76bd6d5f", "JohnDoe@gmail.com", true, "John", new DateTime(2025, 5, 14, 18, 32, 52, 452, DateTimeKind.Utc).AddTicks(4672), false, "Doe", false, null, null, null, "AQAAAAIAAYagAAAAEMjhq15vx0leRDVRBn7hAPhc5s0Omjqwh4WrHx+kRwZAjlENcWbWI+BEnBmswzvTaQ==", null, false, "d7459567-155a-47a1-959e-cdbd5170b6a3", false, "JohnDoe@gmail.com", "..." });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizRooms_Quiz_QuizId",
                table: "QuizRooms",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizRooms_Rooms_RoomId",
                table: "QuizRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizRooms_Quiz_QuizId",
                table: "QuizRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizRooms_Rooms_RoomId",
                table: "QuizRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizRooms",
                table: "QuizRooms");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0");

            migrationBuilder.RenameTable(
                name: "QuizRooms",
                newName: "QuizRoom");

            migrationBuilder.RenameIndex(
                name: "IX_QuizRooms_RoomId",
                table: "QuizRoom",
                newName: "IX_QuizRoom_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizRoom",
                table: "QuizRoom",
                columns: new[] { "QuizId", "RoomId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizRoom_Quiz_QuizId",
                table: "QuizRoom",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizRoom_Rooms_RoomId",
                table: "QuizRoom",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
