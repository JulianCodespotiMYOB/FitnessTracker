using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTargetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetDuration",
                table: "Data");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TargetDuration",
                table: "Data",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
