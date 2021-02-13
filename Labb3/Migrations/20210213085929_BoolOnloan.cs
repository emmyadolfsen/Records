using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb3.Migrations
{
    public partial class BoolOnloan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Onloan",
                table: "Record",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Onloan",
                table: "Record");
        }
    }
}
