using Microsoft.EntityFrameworkCore.Migrations;

namespace LEotA.Engine.Data.Migrations
{
    public partial class Event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "EventParticipants",
                newName: "EventParticipant");

            migrationBuilder.RenameTable(
                name: "Albums",
                newName: "Album");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Event",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Album",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventParticipant",
                table: "EventParticipant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventParticipant",
                table: "EventParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "EventParticipant",
                newName: "EventParticipants");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Albums");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "Id");
        }
    }
}
