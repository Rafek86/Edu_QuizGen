using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class addQuizResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Students_StduentId",
                table: "QuizResults");

            migrationBuilder.RenameColumn(
                name: "StduentId",
                table: "QuizResults",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizResults_StduentId",
                table: "QuizResults",
                newName: "IX_QuizResults_StudentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "QuizResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "QuizResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Quiz",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndAt", "StartAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 13, 12, 4, 51, 138, DateTimeKind.Unspecified).AddTicks(4957), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 13, 12, 4, 51, 138, DateTimeKind.Unspecified).AddTicks(4951), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92b048e3-c5c7-42ab-b2f5-cc8bb44a780a", "AQAAAAIAAYagAAAAEH626vv17dL+0FnQtvo9h2uXzzCi85LRw49oyzrMlcXmc27FaIhalPIq3W+lYgPkVw==", "598bbe6b-25de-4d4a-8d16-69f99b1adeff" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34dbb565-54f7-4b5e-8e43-4c7ed6423725", new DateTime(2025, 6, 13, 12, 4, 51, 353, DateTimeKind.Utc).AddTicks(9940), "AQAAAAIAAYagAAAAEHE6aMtoG0id1fgFS+oLVemRhKRPYNN0jG3Ns7tvvqoeZgWdAwuJBMCbONd5zs22iQ==", "b424d6ed-d6c3-4412-852d-69b79f5a29e0" });

            migrationBuilder.CreateIndex(
                name: "IX_QuizResults_QuizId",
                table: "QuizResults",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_Quiz_QuizId",
                table: "QuizResults",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_Students_StudentId",
                table: "QuizResults",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Quiz_QuizId",
                table: "QuizResults");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Students_StudentId",
                table: "QuizResults");

            migrationBuilder.DropIndex(
                name: "IX_QuizResults_QuizId",
                table: "QuizResults");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "QuizResults");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "QuizResults");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "QuizResults",
                newName: "StduentId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizResults_StudentId",
                table: "QuizResults",
                newName: "IX_QuizResults_StduentId");

            migrationBuilder.UpdateData(
                table: "Quiz",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndAt", "StartAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 11, 4, 51, 47, 210, DateTimeKind.Unspecified).AddTicks(9194), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 11, 4, 51, 47, 210, DateTimeKind.Unspecified).AddTicks(9188), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13189802-caa5-4f8c-a509-e20ad8c2d754", "AQAAAAIAAYagAAAAEHmH4jpw0D5Vywg2t/srQMotSt8d7P2rlAE4jhlgz3+dF+MLwd8UjtzkFuuZBq9q0A==", "46659124-631c-4483-a3d1-92e6c5b69d26" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "077fa970-ea84-4e74-8112-a2ef53032636", new DateTime(2025, 6, 11, 4, 51, 47, 477, DateTimeKind.Utc).AddTicks(3301), "AQAAAAIAAYagAAAAEIH9BvVWyT9rP9+cvYmzWutDIZkgsm/rP69fkdfAd1G/DI/ak4WJ8nEKwdR9ckvAQQ==", "a48b91be-2865-4c70-a244-365da24bdd26" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_Students_StduentId",
                table: "QuizResults",
                column: "StduentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
