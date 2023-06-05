using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.EntityFrameworkCore.Migrations
{
    public partial class addPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                schema: "dbo",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                schema: "dbo",
                table: "user");
        }
    }
}
