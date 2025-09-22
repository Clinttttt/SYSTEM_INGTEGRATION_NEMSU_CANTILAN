using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "users");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "YearLevel",
                table: "users",
                newName: "Role");

            migrationBuilder.CreateTable(
                name: "facilitatorprofiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoursesTaught = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facilitatorprofiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "studentprofiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentprofiles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "facilitatorprofiles");

            migrationBuilder.DropTable(
                name: "studentprofiles");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "YearLevel");

            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
