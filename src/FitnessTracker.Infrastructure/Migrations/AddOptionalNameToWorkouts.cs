#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddOptionalNameToWorkouts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "Name",
            "Workout",
            "text",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "Name",
            "Workout");
    }
}