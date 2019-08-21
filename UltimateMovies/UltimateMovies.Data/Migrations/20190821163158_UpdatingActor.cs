using Microsoft.EntityFrameworkCore.Migrations;

namespace UltimateMovies.Data.Migrations
{
    public partial class UpdatingActor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImdbUrl",
                table: "Actors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbUrl",
                table: "Actors");
        }
    }
}
