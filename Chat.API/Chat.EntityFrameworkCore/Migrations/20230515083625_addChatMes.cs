using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.EntityFrameworkCore.Migrations
{
    public partial class addChatMes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chatMes",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    messageID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    messageStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chatSessionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mediaFiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emojiSymbols = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    createdby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatMes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chatMes",
                schema: "dbo");
        }
    }
}
