using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dc818173-c9de-4700-a34a-965ae09ad466", null, "admin", "admin" },
                    { "ecba105a-24a1-48a7-8b9e-61317122f42c", null, "seller", "seller" },
                    { "f00e889b-316d-4ca9-a5d4-9506bd18c123", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc818173-c9de-4700-a34a-965ae09ad466");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecba105a-24a1-48a7-8b9e-61317122f42c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f00e889b-316d-4ca9-a5d4-9506bd18c123");
        }
    }
}
