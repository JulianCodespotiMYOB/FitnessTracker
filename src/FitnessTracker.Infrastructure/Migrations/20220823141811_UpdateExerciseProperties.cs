using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExerciseProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exercise");

            migrationBuilder.RenameColumn(
                name: "PrimaryMuscleGroup",
                table: "Exercise",
                newName: "Mechanics");

            migrationBuilder.AddColumn<int>(
                name: "DetailedMuscleGroup",
                table: "Exercise",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Equipment",
                table: "Exercise",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MainMuscleGroup",
                table: "Exercise",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int[]>(
                name: "OtherMuscleGroups",
                table: "Exercise",
                type: "integer[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailedMuscleGroup",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "Equipment",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "MainMuscleGroup",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "OtherMuscleGroups",
                table: "Exercise");

            migrationBuilder.RenameColumn(
                name: "Mechanics",
                table: "Exercise",
                newName: "PrimaryMuscleGroup");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exercise",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
