using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manage_tasks_Database.Migrations
{
    public partial class TaskListProjectRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Task_Lists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Task_Lists_ProjectId",
                table: "Task_Lists",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Lists_Projects_ProjectId",
                table: "Task_Lists",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Lists_Projects_ProjectId",
                table: "Task_Lists");

            migrationBuilder.DropIndex(
                name: "IX_Task_Lists_ProjectId",
                table: "Task_Lists");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Task_Lists");
        }
    }
}
