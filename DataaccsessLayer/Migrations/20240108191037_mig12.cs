using Microsoft.EntityFrameworkCore.Migrations;

namespace DataaccsessLayer.Migrations
{
    public partial class mig12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Statues100",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Statues50",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Statuesinfinty",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statues100",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Statues50",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Statuesinfinty",
                table: "Companies");
        }
    }
}
