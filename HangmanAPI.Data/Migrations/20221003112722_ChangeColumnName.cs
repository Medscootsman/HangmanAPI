using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangmanAPI.Data.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalGuesses",
                table: "Games",
                newName: "TotalIncorrectGuesses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalIncorrectGuesses",
                table: "Games",
                newName: "TotalGuesses");
        }
    }
}
