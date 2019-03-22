using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class DeletedExecutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Users_TaskExecutorId",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_TaskExecutorId",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "TaskExecutorId",
                table: "TaskHistories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskExecutorId",
                table: "TaskHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_TaskExecutorId",
                table: "TaskHistories",
                column: "TaskExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Users_TaskExecutorId",
                table: "TaskHistories",
                column: "TaskExecutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
