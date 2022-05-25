using Microsoft.EntityFrameworkCore.Migrations;

namespace Medicine_Chest.Migrations
{
    public partial class MyFirstMigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PharmaciesId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmaciesId",
                table: "AspNetUsers");
        }
    }
}
