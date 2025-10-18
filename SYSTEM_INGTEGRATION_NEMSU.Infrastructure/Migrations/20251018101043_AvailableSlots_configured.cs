using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AvailableSlots_configured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AvailableSlots",
                table: "course",
                type: "int",
                nullable: false,
                computedColumnSql: "[MaxCapacity] - [TotalEnrolled]",
                stored: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AvailableSlots",
                table: "course",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "[MaxCapacity] - [TotalEnrolled]");
        }
    }
}
