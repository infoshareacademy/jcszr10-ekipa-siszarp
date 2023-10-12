using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manage_tasks_Database.Migrations
{
    public partial class TaskTaskListRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskListId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskListId",
                table: "Tasks",
                column: "TaskListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Task_Lists_TaskListId",
                table: "Tasks",
                column: "TaskListId",
                principalTable: "Task_Lists",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Task_Lists_TaskListId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskListId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskListId",
                table: "Tasks");
        }
    }
}
