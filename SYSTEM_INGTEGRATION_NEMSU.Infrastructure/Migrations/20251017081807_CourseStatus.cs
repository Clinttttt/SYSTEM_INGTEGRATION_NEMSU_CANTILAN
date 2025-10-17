using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseStatus",
                table: "course",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseStatus",
                table: "course");
        }
    }
}
