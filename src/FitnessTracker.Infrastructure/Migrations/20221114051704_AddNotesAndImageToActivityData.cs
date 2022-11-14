using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesAndImageToActivityData : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Data",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Data",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Data_ImageId",
                table: "Data",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Data_Images_ImageId",
                table: "Data",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Data_Images_ImageId",
                table: "Data");

            migrationBuilder.DropIndex(
                name: "IX_Data_ImageId",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "Title_Name",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "Notes",
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
