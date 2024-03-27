using Microsoft.EntityFrameworkCore.Migrations;

namespace DataaccsessLayer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WebsiteUrls_CompanyId",
                table: "WebsiteUrls");

            migrationBuilder.DropColumn(
                name: "WebsiteID",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteUrls_CompanyId",
                table: "WebsiteUrls",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WebsiteUrls_CompanyId",
                table: "WebsiteUrls");

            migrationBuilder.AddColumn<int>(
                name: "WebsiteID",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteUrls_CompanyId",
                table: "WebsiteUrls",
                column: "CompanyId",
                unique: true,
                filter: "[CompanyId] IS NOT NULL");
        }
    }
}
