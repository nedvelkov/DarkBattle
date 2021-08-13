using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkBattle.Data.Migrations
{
    public partial class UpdateUserWithChampionCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Champions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Champions_PlayerId",
                table: "Champions",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Champions_AspNetUsers_PlayerId",
                table: "Champions",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Champions_AspNetUsers_PlayerId",
                table: "Champions");

            migrationBuilder.DropIndex(
                name: "IX_Champions_PlayerId",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Champions");
        }
    }
}
