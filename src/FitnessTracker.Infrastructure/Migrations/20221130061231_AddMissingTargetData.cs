using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingTargetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<int>>(
                name: "ClaimedAchievements",
                table: "Users",
                type: "integer[]",
                nullable: false,
                defaultValue: new List<int>(),
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldDefaultValue: new List<int>());

            migrationBuilder.DropColumn(
                name: "Title_Name",
                table: "Rewards");

            migrationBuilder.AddColumn<string>(
                name: "Title_Name",
                table: "Rewards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TargetDuration",
                table: "Data",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title_Name",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "TargetDuration",
                table: "Data");

            migrationBuilder.AlterColumn<List<int>>(
                name: "ClaimedAchievements",
                table: "Users",
                type: "integer[]",
                nullable: false,
                defaultValue: new List<int>(),
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldDefaultValue: new List<int>());
        }
    }
}
