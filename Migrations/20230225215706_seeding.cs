using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialClint.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "UserName" },
                values: new object[,]
                {
                    { 1, "mo" },
                    { 2, "ali" },
                    { 3, "saeed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
