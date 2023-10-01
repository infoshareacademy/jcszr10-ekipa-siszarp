using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTaskMaster.Migrations
{
    public partial class ChangesNameOfCompanyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_AspNetUsers_UserId",
                schema: "CstUserMngt",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_AspNetUsers_UserId",
                schema: "CstUserMngt",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AspNetUsers_UserId",
                schema: "CstUserMngt",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_AspNetUsers_UserId",
                schema: "CstUserMngt",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                schema: "CstUserMngt",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "CstUserMngt",
                newName: "CompanyUser",
                newSchema: "CstUserMngt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyUser",
                schema: "CstUserMngt",
                table: "CompanyUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_CompanyUser_UserId",
                schema: "CstUserMngt",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "CstUserMngt",
                principalTable: "CompanyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_CompanyUser_UserId",
                schema: "CstUserMngt",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "CstUserMngt",
                principalTable: "CompanyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_CompanyUser_UserId",
                schema: "CstUserMngt",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "CstUserMngt",
                principalTable: "CompanyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_CompanyUser_UserId",
                schema: "CstUserMngt",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "CstUserMngt",
                principalTable: "CompanyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_CompanyUser_UserId",
                schema: "CstUserMngt",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_CompanyUser_UserId",
                schema: "CstUserMngt",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_CompanyUser_UserId",
                schema: "CstUserMngt",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_CompanyUser_UserId",
                schema: "CstUserMngt",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyUser",
                schema: "CstUserMngt",
                table: "CompanyUser");

            migrationBuilder.RenameTable(
                name: "CompanyUser",
                schema: "CstUserMngt",
                newName: "AspNetUsers",
                newSchema: "CstUserMngt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                schema: "CstUserMngt",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_AspNetUsers_UserId",
                schema: "CstUserMngt",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "CstUserMngt",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_AspNetUsers_UserId",
                schema: "CstUserMngt",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "CstUserMngt",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_AspNetUsers_UserId",
                schema: "CstUserMngt",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "CstUserMngt",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_AspNetUsers_UserId",
                schema: "CstUserMngt",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "CstUserMngt",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
