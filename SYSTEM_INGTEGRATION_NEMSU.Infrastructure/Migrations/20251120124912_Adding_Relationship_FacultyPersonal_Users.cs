using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Relationship_FacultyPersonal_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FacultyPersonalInformationId",
                table: "users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_FacultyPersonalInformationId",
                table: "users",
                column: "FacultyPersonalInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_facultyPersonalInformation_FacultyPersonalInformationId",
                table: "users",
                column: "FacultyPersonalInformationId",
                principalTable: "facultyPersonalInformation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_facultyPersonalInformation_FacultyPersonalInformationId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_FacultyPersonalInformationId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "FacultyPersonalInformationId",
                table: "users");
        }
    }
}
