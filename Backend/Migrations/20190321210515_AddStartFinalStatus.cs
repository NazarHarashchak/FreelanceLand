using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class AddStartFinalStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Tasks_TaskId",
                table: "TaskHistories");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TaskHistories",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FinalTaskStatusId",
                table: "TaskHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartTaskStatusId",
                table: "TaskHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_FinalTaskStatusId",
                table: "TaskHistories",
                column: "FinalTaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_StartTaskStatusId",
                table: "TaskHistories",
                column: "StartTaskStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_TaskStatuses_FinalTaskStatusId",
                table: "TaskHistories",
                column: "FinalTaskStatusId",
                principalTable: "TaskStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_TaskStatuses_StartTaskStatusId",
                table: "TaskHistories",
                column: "StartTaskStatusId",
                principalTable: "TaskStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Tasks_TaskId",
                table: "TaskHistories",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_TaskStatuses_FinalTaskStatusId",
                table: "TaskHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_TaskStatuses_StartTaskStatusId",
                table: "TaskHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Tasks_TaskId",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_FinalTaskStatusId",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_StartTaskStatusId",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "FinalTaskStatusId",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "StartTaskStatusId",
                table: "TaskHistories");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TaskHistories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Tasks_TaskId",
                table: "TaskHistories",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
