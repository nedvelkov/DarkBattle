using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkBattle.Data.Migrations
{
    public partial class AddNewDbsetChampionClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agility",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "ChampionClass",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "Health",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "SpellPower",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "Strenght",
                table: "Champions");

            migrationBuilder.AddColumn<string>(
                name: "ChampionClassId",
                table: "Champions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ChampionClasses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strenght = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    SpellPower = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Champions_ChampionClassId",
                table: "Champions",
                column: "ChampionClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Champions_ChampionClasses_ChampionClassId",
                table: "Champions",
                column: "ChampionClassId",
                principalTable: "ChampionClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Champions_ChampionClasses_ChampionClassId",
                table: "Champions");

            migrationBuilder.DropTable(
                name: "ChampionClasses");

            migrationBuilder.DropIndex(
                name: "IX_Champions_ChampionClassId",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "ChampionClassId",
                table: "Champions");

            migrationBuilder.AddColumn<int>(
                name: "Agility",
                table: "Champions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChampionClass",
                table: "Champions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Health",
                table: "Champions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpellPower",
                table: "Champions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strenght",
                table: "Champions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
