using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDeals.Catalog.Infrastructure.Migrations
{
    public partial class AddedDiscountToShoppingSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "ShoppingSessions",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "ShoppingSessions");
        }
    }
}
