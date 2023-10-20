using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manage_tasks_Database.Migrations
{
    public partial class TaskTaskListRelationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Task_Lists_TaskListId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Task_Lists_TaskListId",
                table: "Tasks",
                column: "TaskListId",
                principalTable: "Task_Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Task_Lists_TaskListId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Task_Lists_TaskListId",
                table: "Tasks",
                column: "TaskListId",
                principalTable: "Task_Lists",
                principalColumn: "Id");
        }
    }
}
