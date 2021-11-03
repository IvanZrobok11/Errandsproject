using Microsoft.EntityFrameworkCore.Migrations;

namespace Errands.Data.Migrations
{
    public partial class AddConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "FileModels",
                newName: "path");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FileModels",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FileModels",
                newName: "nameFile");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "FileModels",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "path",
                table: "FileModels",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "FileModels",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nameFile",
                table: "FileModels",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "FileModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
