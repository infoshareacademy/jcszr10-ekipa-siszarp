using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manage_tasks_Database.Migrations
{
    public partial class TeamLeaderRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LeaderId",
                table: "Teams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeaderId",
                table: "Teams",
                column: "LeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_LeaderId",
                table: "Teams",
                column: "LeaderId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_LeaderId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LeaderId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Teams");
        }
    }
}
