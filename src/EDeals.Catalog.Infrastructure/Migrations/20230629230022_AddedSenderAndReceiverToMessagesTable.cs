using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDeals.Catalog.Infrastructure.Migrations
{
    public partial class AddedSenderAndReceiverToMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Messages",
                newName: "Sender");

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Messages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "Messages",
                newName: "Username");
        }
    }
}
