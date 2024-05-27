using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Authy.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "705c31a5-e8e0-48f7-92e7-852f4f955ec5", null, "Seller", "seller" },
                    { "ab5e3950-0b90-4852-90bd-0be337ed8971", null, "Client", "client" },
                    { "e0ff639a-b730-422d-b2b8-6c4ae0e6c2d9", null, "Administrator", "administrator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "705c31a5-e8e0-48f7-92e7-852f4f955ec5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab5e3950-0b90-4852-90bd-0be337ed8971");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0ff639a-b730-422d-b2b8-6c4ae0e6c2d9");
        }
    }
}
