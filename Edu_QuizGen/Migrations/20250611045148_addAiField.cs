using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class addAiField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartAt",
                table: "Quiz",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndAt",
                table: "Quiz",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AI",
                table: "Quiz",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Quiz",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AI", "EndAt", "StartAt" },
                values: new object[] { false, new DateTimeOffset(new DateTime(2025, 6, 11, 4, 51, 47, 210, DateTimeKind.Unspecified).AddTicks(9194), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 11, 4, 51, 47, 210, DateTimeKind.Unspecified).AddTicks(9188), new TimeSpan(0, 0, 0, 0, 0)) });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AI",
                table: "Quiz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartAt",
                table: "Quiz",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndAt",
                table: "Quiz",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Quiz",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndAt", "StartAt" },
                values: new object[] { new DateTime(2025, 6, 8, 16, 37, 47, 195, DateTimeKind.Utc).AddTicks(8291), new DateTime(2025, 6, 8, 16, 37, 47, 195, DateTimeKind.Utc).AddTicks(8287) });

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
    }
}
