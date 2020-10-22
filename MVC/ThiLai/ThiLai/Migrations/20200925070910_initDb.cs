using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThiLai.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cakes",
                columns: table => new
                {
                    CakeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBanh = table.Column<string>(nullable: false),
                    ThanhPhan = table.Column<string>(nullable: false),
                    HSD = table.Column<DateTime>(nullable: false),
                    NSX = table.Column<DateTime>(nullable: false),
                    GiaBan = table.Column<int>(nullable: false),
                    DaXoa = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cakes", x => x.CakeId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Bánh Gạo" },
                    { 2, "Bánh Tráng" },
                    { 3, "Bánh ép" },
                    { 4, "Bánh Canh" },
                    { 5, "Bánh Tráng Cuộn" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cakes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
