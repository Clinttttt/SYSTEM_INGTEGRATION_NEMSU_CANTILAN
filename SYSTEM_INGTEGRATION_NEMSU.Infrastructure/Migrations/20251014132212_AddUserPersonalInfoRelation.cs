using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPersonalInfoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_personalInformation_StudentsDetailsId",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "FK_users_personalInformation_StudentsDetailsId",
                table: "users",
                column: "StudentsDetailsId",
                principalTable: "personalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_personalInformation_StudentsDetailsId",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "FK_users_personalInformation_StudentsDetailsId",
                table: "users",
                column: "StudentsDetailsId",
                principalTable: "personalInformation",
                principalColumn: "Id");
        }
    }
}
