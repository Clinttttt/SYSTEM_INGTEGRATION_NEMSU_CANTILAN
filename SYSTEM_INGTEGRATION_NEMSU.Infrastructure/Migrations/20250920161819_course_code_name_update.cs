using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class course_code_name_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "invoice",
                newName: "CourseCode");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "enrollcourse",
                newName: "CourseCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseCode",
                table: "invoice",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "CourseCode",
                table: "enrollcourse",
                newName: "CourseID");
        }
    }
}
