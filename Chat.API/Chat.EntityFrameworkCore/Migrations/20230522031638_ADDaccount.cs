using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.EntityFrameworkCore.Migrations
{
    public partial class ADDaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "account",
                schema: "dbo",
                table: "onlineuser",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "account",
                schema: "dbo",
                table: "onlineuser");
        }
    }
}
