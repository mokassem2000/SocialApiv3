using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using SocialClint.entity;

#nullable disable

namespace SocialClint.Migrations
{
    /// <inheritdoc />
    public partial class seedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "AppUser",
                columns: new string[] { "UserName", "Gender", "Introduction" },
                 values: new AppUser[] {
                    new AppUser() {UserName="mokassem",Gender= "male",Introduction= "i like python" } ,
                    new AppUser() {UserName="saeed",Gender= "male",Introduction= "i like java" },
                    new AppUser() {UserName="sama",Gender= "femail",Introduction= "i like ruby" }
                   }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

        }
    }
}
