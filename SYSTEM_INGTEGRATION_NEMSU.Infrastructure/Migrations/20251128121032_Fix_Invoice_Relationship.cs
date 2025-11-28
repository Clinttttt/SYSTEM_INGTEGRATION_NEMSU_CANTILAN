using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Invoice_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "enrollcourse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_enrollcourse_InvoiceId",
                table: "enrollcourse",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_enrollcourse_invoice_InvoiceId",
                table: "enrollcourse",
                column: "InvoiceId",
                principalTable: "invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enrollcourse_invoice_InvoiceId",
                table: "enrollcourse");

            migrationBuilder.DropIndex(
                name: "IX_enrollcourse_InvoiceId",
                table: "enrollcourse");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "enrollcourse");
        }
    }
}
