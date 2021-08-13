using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkBattle.Data.Migrations
{
    public partial class UpdateItemWithEquippedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Equipped",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipped",
                table: "Items");
        }
    }
}
