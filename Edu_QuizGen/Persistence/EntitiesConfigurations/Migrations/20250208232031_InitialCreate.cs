using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019488ef-b9bd-7b5c-adeb-997f2443d22b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEC4ZEBbI3RlSh5QZ/OF4pxLETYkOPDskgfPi1RMqVzB1Zi5JkWje0lpf22I0HE43yg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019488ef-b9bd-7b5c-adeb-997f2443d22b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMzXJKRb0dqbeSsSTJvjTQm5CNyJ0O8oGPN+CCBwWUj62xJdkJ32MigPtavxLraslg==");
        }
    }
}
