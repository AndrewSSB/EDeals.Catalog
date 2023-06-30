using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDeals.Catalog.Infrastructure.Migrations
{
    public partial class AddedCommentsSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReview",
                table: "ProductReview",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReview",
                table: "ProductReview");
        }
    }
}
