using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UltimateMovies.Data.Migrations
{
    public partial class UpdatingActorsAndMoviesByDelImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Images_PictureId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Images_PosterId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Movies_PosterId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Actors_PictureId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "PosterId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                table: "Movies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Actors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterUrl",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Actors");

            migrationBuilder.AddColumn<int>(
                name: "PosterId",
                table: "Movies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Actors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_PosterId",
                table: "Movies",
                column: "PosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_PictureId",
                table: "Actors",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Images_PictureId",
                table: "Actors",
                column: "PictureId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Images_PosterId",
                table: "Movies",
                column: "PosterId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
