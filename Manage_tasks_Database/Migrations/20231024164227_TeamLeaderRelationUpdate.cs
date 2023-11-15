using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manage_tasks_Database.Migrations
{
    public partial class TeamLeaderRelationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_LeaderId",
                table: "Teams");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_LeaderId",
                table: "Teams",
                column: "LeaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_LeaderId",
                table: "Teams");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_LeaderId",
                table: "Teams",
                column: "LeaderId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
