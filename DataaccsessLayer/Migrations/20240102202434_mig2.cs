using Microsoft.EntityFrameworkCore.Migrations;

namespace DataaccsessLayer.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebsiteUrls_WebsiteUrls_WebsiteUrlWebsiteID",
                table: "WebsiteUrls");

            migrationBuilder.DropIndex(
                name: "IX_WebsiteUrls_WebsiteUrlWebsiteID",
                table: "WebsiteUrls");

            migrationBuilder.DropColumn(
                name: "WebsiteUrlWebsiteID",
                table: "WebsiteUrls");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WebsiteUrlWebsiteID",
                table: "WebsiteUrls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteUrls_WebsiteUrlWebsiteID",
                table: "WebsiteUrls",
                column: "WebsiteUrlWebsiteID");

            migrationBuilder.AddForeignKey(
                name: "FK_WebsiteUrls_WebsiteUrls_WebsiteUrlWebsiteID",
                table: "WebsiteUrls",
                column: "WebsiteUrlWebsiteID",
                principalTable: "WebsiteUrls",
                principalColumn: "WebsiteID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
