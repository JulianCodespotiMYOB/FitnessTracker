using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "Data",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Sets",
                table: "Data",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Reps",
                table: "Data",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Data",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "Data",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<double>(
                name: "TargetDistance",
                table: "Data",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetDuration",
                table: "Data",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TargetReps",
                table: "Data",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetSets",
                table: "Data",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TargetWeight",
                table: "Data",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetDistance",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "TargetDuration",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "TargetReps",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "TargetSets",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "TargetWeight",
                table: "Data");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Data",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sets",
                table: "Data",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Reps",
                table: "Data",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Data",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "Data",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
