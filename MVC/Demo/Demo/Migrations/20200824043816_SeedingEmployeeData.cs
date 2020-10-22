using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class SeedingEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AvatarPath", "Department", "Email", "Fullname" },
                values: new object[] { 1, "~/images/2.png", 0, "anpro@gmail.com", "An Nguyễn" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AvatarPath", "Department", "Email", "Fullname" },
                values: new object[] { 2, "~/images/3.png", 1, "long9x@gmail.com", "Long Nguyễn" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
