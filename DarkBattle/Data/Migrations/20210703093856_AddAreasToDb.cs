using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkBattle.Data.Migrations
{
    public partial class AddAreasToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaId",
                table: "Creatures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_AreaId",
                table: "Creatures",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Areas_AreaId",
                table: "Creatures",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Areas_AreaId",
                table: "Creatures");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Creatures_AreaId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Creatures");
        }
    }
}
