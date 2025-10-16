using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class linked_user_to_academicinformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_academicInformation_StudentAcademicDetailsId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_StudentAcademicDetailsId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "StudentAcademicDetailsId",
                table: "users",
                newName: "StudentAcademicsId");

            migrationBuilder.CreateIndex(
                name: "IX_users_StudentAcademicsId",
                table: "users",
                column: "StudentAcademicsId",
                unique: true,
                filter: "[StudentAcademicsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_users_academicInformation_StudentAcademicsId",
                table: "users",
                column: "StudentAcademicsId",
                principalTable: "academicInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_academicInformation_StudentAcademicsId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_StudentAcademicsId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "StudentAcademicsId",
                table: "users",
                newName: "StudentAcademicDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_users_StudentAcademicDetailsId",
                table: "users",
                column: "StudentAcademicDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_academicInformation_StudentAcademicDetailsId",
                table: "users",
                column: "StudentAcademicDetailsId",
                principalTable: "academicInformation",
                principalColumn: "Id");
        }
    }
}
