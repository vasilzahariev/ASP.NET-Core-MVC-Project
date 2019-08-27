using Microsoft.EntityFrameworkCore.Migrations;

namespace UltimateMovies.Data.Migrations
{
    public partial class AddingAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_AspNetUsers_UMUserId",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "UMUserId",
                table: "Addresses",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "Information",
                table: "Addresses",
                newName: "Street");

            migrationBuilder.RenameIndex(
                name: "IX_Address_UMUserId",
                table: "Addresses",
                newName: "IX_Addresses_UserId1");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "Addresses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId1",
                table: "Addresses",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId1",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Address",
                newName: "UMUserId");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Address",
                newName: "Information");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_UserId1",
                table: "Address",
                newName: "IX_Address_UMUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AspNetUsers_UMUserId",
                table: "Address",
                column: "UMUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
