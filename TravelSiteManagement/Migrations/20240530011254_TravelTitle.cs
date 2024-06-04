using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelSiteWeb.Migrations
{
    /// <inheritdoc />
    public partial class TravelTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TravelTitle",
                table: "TravelDestination",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelTitle",
                table: "TravelDestination");
        }
    }
}
