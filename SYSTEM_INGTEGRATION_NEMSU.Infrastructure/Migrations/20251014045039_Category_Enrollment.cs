using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Category_Enrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "enrollcourse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_enrollcourse_CategoryId",
                table: "enrollcourse",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_enrollcourse_category_CategoryId",
                table: "enrollcourse",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enrollcourse_category_CategoryId",
                table: "enrollcourse");

            migrationBuilder.DropIndex(
                name: "IX_enrollcourse_CategoryId",
                table: "enrollcourse");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "enrollcourse");
        }
    }
}
