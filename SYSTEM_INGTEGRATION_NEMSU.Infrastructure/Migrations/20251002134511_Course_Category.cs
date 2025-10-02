using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Course_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "course",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Color", "Icon", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "teal", "📐", "Mathematics" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "purple", "🎨", "Arts & Humanities" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "blue", "💻", "Computer Science" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "orange", "📚", "Social Sciences" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "gray", "⚗️", "Natural Sciences" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_course_CategoryId",
                table: "course",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_course_Category_CategoryId",
                table: "course",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_Category_CategoryId",
                table: "course");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_course_CategoryId",
                table: "course");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "course");
        }
    }
}
