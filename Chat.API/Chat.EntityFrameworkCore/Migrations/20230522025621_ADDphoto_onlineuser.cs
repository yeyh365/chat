using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.EntityFrameworkCore.Migrations
{
    public partial class ADDphoto_onlineuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photo",
                schema: "dbo",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photo",
                schema: "dbo",
                table: "onlineuser",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo",
                schema: "dbo",
                table: "user");

            migrationBuilder.DropColumn(
                name: "photo",
                schema: "dbo",
                table: "onlineuser");
        }
    }
}
