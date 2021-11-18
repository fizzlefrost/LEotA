using Microsoft.EntityFrameworkCore.Migrations;

namespace LEotA.Engine.Data.Migrations
{
    public partial class FixStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Staff");
        }
    }
}
