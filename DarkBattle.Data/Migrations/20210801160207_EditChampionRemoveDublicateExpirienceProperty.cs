using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkBattle.Data.Migrations
{
    public partial class EditChampionRemoveDublicateExpirienceProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expirence",
                table: "Champions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Expirence",
                table: "Champions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
