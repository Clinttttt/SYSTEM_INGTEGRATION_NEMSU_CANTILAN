using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updating_category_courses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Name",
                value: "Mathematics & Statistics");

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Color", "Icon", "Name" },
                values: new object[] { "blue", "💻", "Computer Science & IT" });

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "Color", "Icon", "Name" },
                values: new object[] { "green", "⚗️", "Natural Sciences" });

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Icon",
                value: "🌍");

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "Color", "Icon", "Name" },
                values: new object[] { "purple", "🎨", "Arts & Humanities" });

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "Id", "Color", "Icon", "Name" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666666"), "indigo", "💼", "Business & Management" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "pink", "💬", "Languages & Communication" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Name",
                value: "Mathematics");

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Color", "Icon", "Name" },
                values: new object[] { "purple", "🎨", "Arts & Humanities" });

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "Color", "Icon", "Name" },
                values: new object[] { "blue", "💻", "Computer Science" });

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Icon",
                value: "📚");

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "Color", "Icon", "Name" },
                values: new object[] { "gray", "⚗️", "Natural Sciences" });
        }
    }
}
