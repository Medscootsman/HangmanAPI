using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangmanAPI.Data.Migrations
{
    public partial class ChangeCountToBeTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalGuesses",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalGuesses",
                table: "Games");
        }
    }
}
