using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConsolidateChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Data_DataId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Exercises_ExerciseId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Workouts_WorkoutId",
                table: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activity",
                table: "Activity");

            migrationBuilder.RenameTable(
                name: "Activity",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_WorkoutId",
                table: "Activities",
                newName: "IX_Activities_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_ExerciseId",
                table: "Activities",
                newName: "IX_Activities_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_DataId",
                table: "Activities",
                newName: "IX_Activities_DataId");

            migrationBuilder.AlterColumn<List<int>>(
                name: "ClaimedAchievements",
                table: "Users",
                type: "integer[]",
                nullable: false,
                defaultValue: new List<int>(),
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldDefaultValue: new List<int>());

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Data_DataId",
                table: "Activities",
                column: "DataId",
                principalTable: "Data",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Exercises_ExerciseId",
                table: "Activities",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Workouts_WorkoutId",
                table: "Activities",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Data_DataId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Exercises_ExerciseId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Workouts_WorkoutId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Title_Name",
                table: "Rewards");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "Activity");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_WorkoutId",
                table: "Activity",
                newName: "IX_Activity_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_ExerciseId",
                table: "Activity",
                newName: "IX_Activity_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_DataId",
                table: "Activity",
                newName: "IX_Activity_DataId");

            migrationBuilder.AlterColumn<List<int>>(
                name: "ClaimedAchievements",
                table: "Users",
                type: "integer[]",
                nullable: false,
                defaultValue: new List<int>(),
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldDefaultValue: new List<int>());

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activity",
                table: "Activity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Data_DataId",
                table: "Activity",
                column: "DataId",
                principalTable: "Data",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Exercises_ExerciseId",
                table: "Activity",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Workouts_WorkoutId",
                table: "Activity",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }
    }
}
