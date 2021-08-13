using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkBattle.Data.Migrations
{
    public partial class ChangeNameOfColumnInConsumbale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChampionConsumable_Champions_ChampionConsumablesId",
                table: "ChampionConsumable");

            migrationBuilder.DropForeignKey(
                name: "FK_ChampionConsumable_Consumables_ChampionConsumablesId1",
                table: "ChampionConsumable");

            migrationBuilder.RenameColumn(
                name: "ChampionConsumablesId1",
                table: "ChampionConsumable",
                newName: "ChampionsId");

            migrationBuilder.RenameIndex(
                name: "IX_ChampionConsumable_ChampionConsumablesId1",
                table: "ChampionConsumable",
                newName: "IX_ChampionConsumable_ChampionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChampionConsumable_Champions_ChampionsId",
                table: "ChampionConsumable",
                column: "ChampionsId",
                principalTable: "Champions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChampionConsumable_Consumables_ChampionConsumablesId",
                table: "ChampionConsumable",
                column: "ChampionConsumablesId",
                principalTable: "Consumables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChampionConsumable_Champions_ChampionsId",
                table: "ChampionConsumable");

            migrationBuilder.DropForeignKey(
                name: "FK_ChampionConsumable_Consumables_ChampionConsumablesId",
                table: "ChampionConsumable");

            migrationBuilder.RenameColumn(
                name: "ChampionsId",
                table: "ChampionConsumable",
                newName: "ChampionConsumablesId1");

            migrationBuilder.RenameIndex(
                name: "IX_ChampionConsumable_ChampionsId",
                table: "ChampionConsumable",
                newName: "IX_ChampionConsumable_ChampionConsumablesId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ChampionConsumable_Champions_ChampionConsumablesId",
                table: "ChampionConsumable",
                column: "ChampionConsumablesId",
                principalTable: "Champions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChampionConsumable_Consumables_ChampionConsumablesId1",
                table: "ChampionConsumable",
                column: "ChampionConsumablesId1",
                principalTable: "Consumables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
