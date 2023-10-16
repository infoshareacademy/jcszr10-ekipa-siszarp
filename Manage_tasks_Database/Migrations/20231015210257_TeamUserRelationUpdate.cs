using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manage_tasks_Database.Migrations
{
    public partial class TeamUserRelationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_Teams_UserId",
                table: "Teams_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_Users_TeamId",
                table: "Teams_Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_Teams_TeamId",
                table: "Teams_Users",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_Users_UserId",
                table: "Teams_Users",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_Teams_TeamId",
                table: "Teams_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_Users_UserId",
                table: "Teams_Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_Teams_UserId",
                table: "Teams_Users",
                column: "UserId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_Users_TeamId",
                table: "Teams_Users",
                column: "TeamId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
