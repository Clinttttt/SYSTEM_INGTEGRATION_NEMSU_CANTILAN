using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adding_StudentsDetailsId_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_StudentsDetailsId",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "IX_users_StudentsDetailsId",
                table: "users",
                column: "StudentsDetailsId",
                unique: true,
                filter: "[StudentsDetailsId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_StudentsDetailsId",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "IX_users_StudentsDetailsId",
                table: "users",
                column: "StudentsDetailsId");
        }
    }
}
