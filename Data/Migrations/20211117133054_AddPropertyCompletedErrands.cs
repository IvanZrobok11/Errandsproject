using Microsoft.EntityFrameworkCore.Migrations;

namespace Errands.Data.Migrations
{
    public partial class AddPropertyCompletedErrands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedErrands",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedErrands",
                table: "AspNetUsers");
        }
    }
}
