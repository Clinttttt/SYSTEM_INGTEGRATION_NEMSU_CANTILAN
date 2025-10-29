using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adding_relationship_course_facultypersonaldetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_facultyPersonalInformation_FacultyPersonalsId",
                table: "course");

            migrationBuilder.AddForeignKey(
                name: "FK_course_facultyPersonalInformation_FacultyPersonalsId",
                table: "course",
                column: "FacultyPersonalsId",
                principalTable: "facultyPersonalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_facultyPersonalInformation_FacultyPersonalsId",
                table: "course");

            migrationBuilder.AddForeignKey(
                name: "FK_course_facultyPersonalInformation_FacultyPersonalsId",
                table: "course",
                column: "FacultyPersonalsId",
                principalTable: "facultyPersonalInformation",
                principalColumn: "Id");
        }
    }
}
