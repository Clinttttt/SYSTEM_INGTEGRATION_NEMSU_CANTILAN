using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataType_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "enrollcourse");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "announcements");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "enrollcourse",
                newName: "StudentId");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId_FK",
                table: "studentprofiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "invoice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "invoice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Faculty_FK",
                table: "facilitatorprofiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "enrollcourse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "enrollcourse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "announcements",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "announcements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoice_CourseId",
                table: "invoice",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_StudentId",
                table: "invoice",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_enrollcourse_CourseId",
                table: "enrollcourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_enrollcourse_StudentId",
                table: "enrollcourse",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_enrollcourse_course_CourseId",
                table: "enrollcourse",
                column: "CourseId",
                principalTable: "course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollcourse_users_StudentId",
                table: "enrollcourse",
                column: "StudentId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_course_CourseId",
                table: "invoice",
                column: "CourseId",
                principalTable: "course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_users_StudentId",
                table: "invoice",
                column: "StudentId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enrollcourse_course_CourseId",
                table: "enrollcourse");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollcourse_users_StudentId",
                table: "enrollcourse");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_course_CourseId",
                table: "invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_users_StudentId",
                table: "invoice");

            migrationBuilder.DropIndex(
                name: "IX_invoice_CourseId",
                table: "invoice");

            migrationBuilder.DropIndex(
                name: "IX_invoice_StudentId",
                table: "invoice");

            migrationBuilder.DropIndex(
                name: "IX_enrollcourse_CourseId",
                table: "enrollcourse");

            migrationBuilder.DropIndex(
                name: "IX_enrollcourse_StudentId",
                table: "enrollcourse");

            migrationBuilder.DropColumn(
                name: "StudentId_FK",
                table: "studentprofiles");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "invoice");

            migrationBuilder.DropColumn(
                name: "Faculty_FK",
                table: "facilitatorprofiles");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "enrollcourse");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "announcements");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "enrollcourse",
                newName: "StudentID");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "invoice",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "enrollcourse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "enrollcourse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "announcements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "announcements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
