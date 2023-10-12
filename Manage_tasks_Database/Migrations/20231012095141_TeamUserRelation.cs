using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manage_tasks_Database.Migrations
{
    public partial class TeamUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams_Users",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams_Users", x => new { x.TeamId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Teams_Users_Teams_UserId",
                        column: x => x.UserId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Users_Users_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Users_UserId",
                table: "Teams_Users",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams_Users");
        }
    }
}
