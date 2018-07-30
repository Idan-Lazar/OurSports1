using Microsoft.EntityFrameworkCore.Migrations;

namespace OurSports1.Data.Migrations
{
    public partial class maps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Stadiums",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumHouse",
                table: "Stadiums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Stadiums",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Stadiums",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "NumHouse",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Stadiums");
        }
    }
}
