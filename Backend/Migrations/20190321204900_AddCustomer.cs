using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class AddCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "Tasks",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Tasks",
                newName: "DateCreate");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CustomerId",
                table: "Tasks",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_CustomerId",
                table: "Tasks",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_CustomerId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CustomerId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Tasks",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "DateCreate",
                table: "Tasks",
                newName: "Date");
        }
    }
}
