using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelSiteWeb.Migrations
{
    /// <inheritdoc />
    public partial class Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "TravelDestination");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Cost",
                table: "TravelDestination",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
