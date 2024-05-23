using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChinesOuction.Migrations
{
    public partial class INITdB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Present",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Present_CustomerID",
                table: "Present",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Present_Customer_CustomerID",
                table: "Present",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Present_Customer_CustomerID",
                table: "Present");

            migrationBuilder.DropIndex(
                name: "IX_Present_CustomerID",
                table: "Present");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Present");
        }
    }
}
