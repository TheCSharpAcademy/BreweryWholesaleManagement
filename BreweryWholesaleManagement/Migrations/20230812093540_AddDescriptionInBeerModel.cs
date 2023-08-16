using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryWholesaleManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionInBeerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Beer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Beer");
        }
    }
}
