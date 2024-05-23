using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectChinesOuction.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(//יצירת הטבלה
                name: "Customer",//שם/סוג הטבלה
                columns: table => new// פירוט על העמודות של טבלה זו
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 5"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Custemail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);//KEY
                });

            migrationBuilder.CreateTable(
                name: "Donator",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donator", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Present",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumOfCust = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Present", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Donator");

            migrationBuilder.DropTable(
                name: "Present");
        }
    }
}
