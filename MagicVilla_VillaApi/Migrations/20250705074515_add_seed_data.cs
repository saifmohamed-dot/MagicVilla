using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class add_seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amentiy", "DateCreated", "DateUpdated", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[,]
                {
                    { 1, "very good", new DateTime(2025, 7, 5, 10, 45, 15, 194, DateTimeKind.Local).AddTicks(2446), new DateTime(2025, 7, 5, 10, 45, 15, 197, DateTimeKind.Local).AddTicks(1025), "this Is a pritty good villa", "https://localhost/dev/tech", "VillaDia", 5, 4.2999999999999998, 3003 },
                    { 2, "meduim", new DateTime(2025, 7, 5, 10, 45, 15, 197, DateTimeKind.Local).AddTicks(1347), new DateTime(2025, 7, 5, 10, 45, 15, 197, DateTimeKind.Local).AddTicks(1354), "this Is a meduim good villa", "https://localhost/dev/tech", "VillaCont", 3, 3.3999999999999999, 2500 },
                    { 3, "bad", new DateTime(2025, 7, 5, 10, 45, 15, 197, DateTimeKind.Local).AddTicks(1357), new DateTime(2025, 7, 5, 10, 45, 15, 197, DateTimeKind.Local).AddTicks(1358), "this Is a bad villa", "https://localhost/dev/tech", "TalaVilla", 5, 2.1000000000000001, 1500 },
                    { 4, "very Smoked", new DateTime(2025, 7, 5, 10, 45, 15, 197, DateTimeKind.Local).AddTicks(1363), new DateTime(2025, 7, 5, 10, 45, 15, 197, DateTimeKind.Local).AddTicks(1365), "this Is a Smoked villa", "https://localhost/dev/tech", "CockedVilla", 5, 3.2999999999999998, 1000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
