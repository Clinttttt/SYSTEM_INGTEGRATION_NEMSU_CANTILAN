using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adding_academicdetails_in_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentAcademicDetailsId",
                table: "users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_StudentAcademicDetailsId",
                table: "users",
                column: "StudentAcademicDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_academicInformation_StudentAcademicDetailsId",
                table: "users",
                column: "StudentAcademicDetailsId",
                principalTable: "academicInformation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_academicInformation_StudentAcademicDetailsId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_StudentAcademicDetailsId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "StudentAcademicDetailsId",
                table: "users");
        }
    }
}
