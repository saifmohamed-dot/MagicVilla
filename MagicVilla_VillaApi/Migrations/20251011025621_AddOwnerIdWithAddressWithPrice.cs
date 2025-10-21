using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIdWithAddressWithPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.AddColumn<int>(
            //    name: "OwnerId",
            //    table: "Villas",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Villas",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "OwnerId", "Price" },
                values: new object[] { "Cairo Egypt", 1, 100.2f });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "OwnerId", "Price" },
                values: new object[] { "Cairo Egypt", 1, 100.2f });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "OwnerId", "Price" },
                values: new object[] { "Cairo Egypt", 1, 100.2f });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Address", "OwnerId", "Price" },
                values: new object[] { "Cairo Egypt", 1, 100.2f });

            migrationBuilder.CreateIndex(
                name: "IX_Villas_OwnerId",
                table: "Villas",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Villas_Users_OwnerId",
                table: "Villas",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Villas_Users_OwnerId",
                table: "Villas");

            migrationBuilder.DropIndex(
                name: "IX_Villas_OwnerId",
                table: "Villas");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Villas");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Villas");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Villas");
        }
    }
}
