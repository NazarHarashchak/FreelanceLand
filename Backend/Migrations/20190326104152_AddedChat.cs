using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class AddedChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_GetterUserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "GetterUserId",
                table: "Messages",
                newName: "ChatRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_GetterUserId",
                table: "Messages",
                newName: "IX_Messages_ChatRoomId");

            migrationBuilder.CreateTable(
                name: "ChatRoom",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: true),
                    SecondUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRoom_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatRoom_Users_SecondUserId",
                        column: x => x.SecondUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoom_CreatorId",
                table: "ChatRoom",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoom_SecondUserId",
                table: "ChatRoom",
                column: "SecondUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatRoom_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId",
                principalTable: "ChatRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatRoom_ChatRoomId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "ChatRoom");

            migrationBuilder.RenameColumn(
                name: "ChatRoomId",
                table: "Messages",
                newName: "GetterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages",
                newName: "IX_Messages_GetterUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_GetterUserId",
                table: "Messages",
                column: "GetterUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
