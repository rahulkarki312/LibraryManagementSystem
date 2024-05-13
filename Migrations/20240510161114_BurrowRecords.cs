using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class BurrowRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "BurrowRecords",
                 columns: table => new
                 {
                     Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                     StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                     BookId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                     BurrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                     ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                     IsFined = table.Column<bool>(type: "bit", nullable: false),
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_BurrowRecords", x => x.Id);
                 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BurrowRecords");
        }
    }
}
