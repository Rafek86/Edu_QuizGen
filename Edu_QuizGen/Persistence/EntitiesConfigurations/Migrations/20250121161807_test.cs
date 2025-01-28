using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_QuizGen.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0194895d-f050-7461-b24f-89be04b4f5ad",
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019488ef-b9bd-7b5c-adeb-997f2443d22b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOtro3sJaSqAzR2f668Vh5b02gWFbirbPU1kIhuI8uw5CXksM9BpfUXrpAjkIdnaeA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0194895d-f050-7461-b24f-89be04b4f5ad",
                column: "IsDefault",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019488ef-b9bd-7b5c-adeb-997f2443d22b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECq5HEmDQPK3OlfsqQWjDJ7iWy09/tUrCN21Un1O4j9OMVILfdlKGtk9+MYN24kSwA==");
        }
    }
}
