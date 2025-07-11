using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Estimator.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRebarSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RebarSize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RebarSize", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RebarSize",
                columns: new[] { "Id", "CategoryId", "Size" },
                values: new object[,]
                {
                    { 3, 2, "3/4" },
                    { 4, 2, "4/8" },
                    { 5, 2, "5/8" },
                    { 6, 2, "3/4" },
                    { 7, 2, "7/8" },
                    { 8, 2, "1" },
                    { 10, 1, "10M" },
                    { 15, 1, "15M" },
                    { 20, 1, "20M" },
                    { 25, 1, "25M" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RebarSize");
        }
    }
}
