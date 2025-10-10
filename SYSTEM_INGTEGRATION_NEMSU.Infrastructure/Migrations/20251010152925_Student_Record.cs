using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Student_Record : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "academicInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentType = table.Column<int>(type: "int", nullable: false),
                    YearLevel = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    Program = table.Column<int>(type: "int", nullable: false),
                    Major = table.Column<int>(type: "int", nullable: false),
                    Strand = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_academicInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contactInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "personalInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CivilStatus = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianContact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personalInformation", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "academicInformation");

            migrationBuilder.DropTable(
                name: "contactInformation");

            migrationBuilder.DropTable(
                name: "personalInformation");
        }
    }
}
