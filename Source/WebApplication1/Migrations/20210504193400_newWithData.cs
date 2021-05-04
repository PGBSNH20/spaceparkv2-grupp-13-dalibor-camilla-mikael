using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class newWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Spaceports",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "DarkPark" });

            migrationBuilder.InsertData(
                table: "Parkings",
                columns: new[] { "Id", "Fee", "MaxLength", "Occupied", "ParkedBy", "ShipName", "SpacePortId" },
                values: new object[,]
                {
                    { 1, 10, 50m, false, null, null, 1 },
                    { 2, 50, 100m, false, null, null, 1 },
                    { 3, 100, 200m, false, null, null, 1 },
                    { 4, 1000, 2000m, false, null, null, 1 },
                    { 5, 5, 15m, false, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parkings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parkings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Parkings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Parkings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parkings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Spaceports",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
