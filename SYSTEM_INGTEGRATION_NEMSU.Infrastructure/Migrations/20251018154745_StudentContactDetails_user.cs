using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentContactDetails_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentContactsId",
                table: "users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_StudentContactsId",
                table: "users",
                column: "StudentContactsId",
                unique: true,
                filter: "[StudentContactsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_users_contactInformation_StudentContactsId",
                table: "users",
                column: "StudentContactsId",
                principalTable: "contactInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_contactInformation_StudentContactsId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_StudentContactsId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "StudentContactsId",
                table: "users");
        }
    }
}
