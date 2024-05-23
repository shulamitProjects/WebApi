using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChinesOuction.Migrations
{
    public partial class aa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Order");

            migrationBuilder.AddColumn<bool>(
                name: "isDraft",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDraft",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
