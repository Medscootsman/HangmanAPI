using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangmanAPI.Data.Migrations
{
    public partial class changeBoolName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameCompleted",
                table: "Games",
                newName: "Completed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "Games",
                newName: "GameCompleted");
        }
    }
}
