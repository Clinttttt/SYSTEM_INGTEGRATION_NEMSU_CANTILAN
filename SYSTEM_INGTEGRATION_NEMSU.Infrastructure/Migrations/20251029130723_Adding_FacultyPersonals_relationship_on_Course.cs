using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adding_FacultyPersonals_relationship_on_Course : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FacultyPersonalsId",
                table: "course",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_course_FacultyPersonalsId",
                table: "course",
                column: "FacultyPersonalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_course_facultyPersonalInformation_FacultyPersonalsId",
                table: "course",
                column: "FacultyPersonalsId",
                principalTable: "facultyPersonalInformation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_facultyPersonalInformation_FacultyPersonalsId",
                table: "course");

            migrationBuilder.DropIndex(
                name: "IX_course_FacultyPersonalsId",
                table: "course");

            migrationBuilder.DropColumn(
                name: "FacultyPersonalsId",
                table: "course");
        }
    }
}
