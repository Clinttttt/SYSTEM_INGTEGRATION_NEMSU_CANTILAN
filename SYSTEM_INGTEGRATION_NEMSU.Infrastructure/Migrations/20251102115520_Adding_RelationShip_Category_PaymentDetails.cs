using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adding_RelationShip_Category_PaymentDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "paymentDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_paymentDetails_CategoryId",
                table: "paymentDetails",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_paymentDetails_category_CategoryId",
                table: "paymentDetails",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paymentDetails_category_CategoryId",
                table: "paymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_paymentDetails_CategoryId",
                table: "paymentDetails");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "paymentDetails");
        }
    }
}
