using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkBattle.Data.Migrations
{
    public partial class AddGearTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GearId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GearId",
                table: "Champions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gears",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChampionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gears_Champions_ChampionId",
                        column: x => x.ChampionId,
                        principalTable: "Champions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_GearId",
                table: "Items",
                column: "GearId");

            migrationBuilder.CreateIndex(
                name: "IX_Gears_ChampionId",
                table: "Gears",
                column: "ChampionId",
                unique: true,
                filter: "[ChampionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Gears_GearId",
                table: "Items",
                column: "GearId",
                principalTable: "Gears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Gears_GearId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Gears");

            migrationBuilder.DropIndex(
                name: "IX_Items_GearId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GearId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GearId",
                table: "Champions");
        }
    }
}
