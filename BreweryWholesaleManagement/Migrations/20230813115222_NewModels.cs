using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryWholesaleManagement.Migrations
{
    public partial class NewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_Brewerys_BreweryId",
                table: "Beer");

            migrationBuilder.AlterColumn<int>(
                name: "BreweryId",
                table: "Beer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_Brewerys_BreweryId",
                table: "Beer",
                column: "BreweryId",
                principalTable: "Brewerys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_Brewerys_BreweryId",
                table: "Beer");

            migrationBuilder.AlterColumn<int>(
                name: "BreweryId",
                table: "Beer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_Brewerys_BreweryId",
                table: "Beer",
                column: "BreweryId",
                principalTable: "Brewerys",
                principalColumn: "Id");
        }
    }
}
