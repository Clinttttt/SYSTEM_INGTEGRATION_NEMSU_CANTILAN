using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Announcement_change_to_AdminId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "announcements",
                newName: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "announcements",
                newName: "StudentId");
        }
    }
}
