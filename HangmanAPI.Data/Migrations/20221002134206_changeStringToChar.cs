using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangmanAPI.Data.Migrations
{
    public partial class changeStringToChar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Guesses_GameId",
                table: "Guesses");

            migrationBuilder.AlterColumn<string>(
                name: "Letter",
                table: "Guesses",
                type: "nvarchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guesses_GameId",
                table: "Guesses",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Guesses_GameId",
                table: "Guesses");

            migrationBuilder.AlterColumn<string>(
                name: "Letter",
                table: "Guesses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guesses_GameId",
                table: "Guesses",
                column: "GameId",
                unique: true);
        }
    }
}
