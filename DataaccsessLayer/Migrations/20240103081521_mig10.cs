using Microsoft.EntityFrameworkCore.Migrations;

namespace DataaccsessLayer.Migrations
{
    public partial class mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "wevsitedown",
                table: "WebsiteUrls",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wevsitedown",
                table: "WebsiteUrls");
        }
    }
}
