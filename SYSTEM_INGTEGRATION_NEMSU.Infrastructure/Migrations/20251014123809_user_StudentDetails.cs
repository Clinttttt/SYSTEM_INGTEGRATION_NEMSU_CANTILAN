using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class user_StudentDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentsDetailsId",
                table: "users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_StudentsDetailsId",
                table: "users",
                column: "StudentsDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_personalInformation_StudentsDetailsId",
                table: "users",
                column: "StudentsDetailsId",
                principalTable: "personalInformation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_personalInformation_StudentsDetailsId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_StudentsDetailsId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "StudentsDetailsId",
                table: "users");
        }
    }
}
