using Microsoft.EntityFrameworkCore.Migrations;

namespace Errands.Data.Migrations
{
    public partial class AddLogoInModelUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FileModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileModels_UserId",
                table: "FileModels",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_AspNetUsers_UserId",
                table: "FileModels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_AspNetUsers_UserId",
                table: "FileModels");

            migrationBuilder.DropIndex(
                name: "IX_FileModels_UserId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FileModels");
        }
    }
}
