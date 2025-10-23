using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_user_in_course : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "course",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_course_userId",
                table: "course",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_course_users_userId",
                table: "course",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_users_userId",
                table: "course");

            migrationBuilder.DropIndex(
                name: "IX_course_userId",
                table: "course");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "course");
        }
    }
}
