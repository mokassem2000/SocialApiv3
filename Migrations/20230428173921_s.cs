using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialClint.Migrations
{
    /// <inheritdoc />
    public partial class s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_AspNetUsers_RecipientId1",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_AspNetUsers_SenderId1",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_RecipientId1",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_SenderId1",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "RecipientId1",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "SenderId1",
                table: "messages");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "messages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RecipientId",
                table: "messages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_messages_RecipientId",
                table: "messages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_SenderId",
                table: "messages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_AspNetUsers_RecipientId",
                table: "messages",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_AspNetUsers_SenderId",
                table: "messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_AspNetUsers_RecipientId",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_AspNetUsers_SenderId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_RecipientId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_SenderId",
                table: "messages");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RecipientId",
                table: "messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RecipientId1",
                table: "messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderId1",
                table: "messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_messages_RecipientId1",
                table: "messages",
                column: "RecipientId1");

            migrationBuilder.CreateIndex(
                name: "IX_messages_SenderId1",
                table: "messages",
                column: "SenderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_AspNetUsers_RecipientId1",
                table: "messages",
                column: "RecipientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_AspNetUsers_SenderId1",
                table: "messages",
                column: "SenderId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
