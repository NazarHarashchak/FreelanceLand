using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class AddUpdatedByUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TaskHistories",
                newName: "DateUpdated");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "TaskHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_UpdatedByUserId",
                table: "TaskHistories",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Users_UpdatedByUserId",
                table: "TaskHistories",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Users_UpdatedByUserId",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_UpdatedByUserId",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "TaskHistories");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "TaskHistories",
                newName: "Date");
        }
    }
}
