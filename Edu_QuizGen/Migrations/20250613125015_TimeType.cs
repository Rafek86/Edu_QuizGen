using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class TimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CompletedAt",
                table: "QuizResults",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Quiz",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndAt", "StartAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 13, 12, 50, 14, 384, DateTimeKind.Unspecified).AddTicks(4934), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 13, 12, 50, 14, 384, DateTimeKind.Unspecified).AddTicks(4929), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a85c6f8-6a68-436b-9a6f-9ea1e92eadce", "AQAAAAIAAYagAAAAEA3mPh9xDcPspgRB6dRv+6cH8Heu4vC2OuVehBQHdWw1LpzD24LOQReSVKfRI9CSdQ==", "40c4fdf0-9455-47eb-ace5-79493a49e339" });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "11caefd4-1787-43cb-92b1-ec7a68d628c0",
                columns: new[] { "ConcurrencyStamp", "HireDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3656164-d256-4e95-b3b5-187aa2b69940", new DateTime(2025, 6, 13, 12, 50, 14, 604, DateTimeKind.Utc).AddTicks(8848), "AQAAAAIAAYagAAAAECyo7zLkFEiBpWxzbUPiEL8+7hlZ3juDiNjqSOqA8v8snSOlDduj0Q9jNAqQA2juoQ==", "38ab88be-c4a9-4e45-b9a9-6b739caac9c5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedAt",
                table: "QuizResults",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

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
        }
    }
}
