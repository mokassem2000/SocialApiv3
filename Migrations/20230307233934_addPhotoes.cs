using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialClint.Migrations
{
    /// <inheritdoc />
    public partial class addPhotoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_AppUserId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "photos");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "photos",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_AppUserId",
                table: "photos",
                newName: "IX_photos_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_photos",
                table: "photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_AspNetUsers_userId",
                table: "photos",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_AspNetUsers_userId",
                table: "photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_photos",
                table: "photos");

            migrationBuilder.RenameTable(
                name: "photos",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Photo",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_photos_userId",
                table: "Photo",
                newName: "IX_Photo_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_AppUserId",
                table: "Photo",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
