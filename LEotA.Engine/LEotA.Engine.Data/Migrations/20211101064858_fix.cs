using Microsoft.EntityFrameworkCore.Migrations;

namespace LEotA.Engine.Data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FileContent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FileContent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
