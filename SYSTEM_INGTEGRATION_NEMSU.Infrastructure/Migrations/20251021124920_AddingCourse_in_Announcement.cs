using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingCourse_in_Announcement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "announcements");

            migrationBuilder.RenameColumn(
                name: "studentCourseStatus",
                table: "enrollcourse",
                newName: "StudentCourseStatus");

            migrationBuilder.CreateIndex(
                name: "IX_announcements_CourseId",
                table: "announcements",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_announcements_course_CourseId",
                table: "announcements",
                column: "CourseId",
                principalTable: "course",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_announcements_course_CourseId",
                table: "announcements");

            migrationBuilder.DropIndex(
                name: "IX_announcements_CourseId",
                table: "announcements");

            migrationBuilder.RenameColumn(
                name: "StudentCourseStatus",
                table: "enrollcourse",
                newName: "studentCourseStatus");

            migrationBuilder.AddColumn<int>(
                name: "Course",
                table: "announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
