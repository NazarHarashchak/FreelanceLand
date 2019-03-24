using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class Editedchatroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "ChatRooms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondUserId",
                table: "ChatRooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_CreatorId",
                table: "ChatRooms",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_SecondUserId",
                table: "ChatRooms",
                column: "SecondUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_CreatorId",
                table: "ChatRooms",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_SecondUserId",
                table: "ChatRooms",
                column: "SecondUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_CreatorId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_SecondUserId",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_CreatorId",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_SecondUserId",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "SecondUserId",
                table: "ChatRooms");
        }
    }
}
