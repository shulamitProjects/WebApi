using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChinesOuction.Migrations
{
    public partial class ADD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Present",
                newName: "PresentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PresentID",
                table: "Present",
                newName: "ID");
        }
    }
}
