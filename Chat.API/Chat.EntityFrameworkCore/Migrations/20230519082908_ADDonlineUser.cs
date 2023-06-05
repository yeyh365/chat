using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.EntityFrameworkCore.Migrations
{
    public partial class ADDonlineUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "onlineuser",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    createdby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_onlineuser", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "onlineuser",
                schema: "dbo");
        }
    }
}
