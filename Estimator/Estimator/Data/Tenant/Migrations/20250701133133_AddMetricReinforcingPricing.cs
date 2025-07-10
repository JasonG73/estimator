using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estimator.Data.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class AddMetricReinforcingPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetricReinforcingPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RebarTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RebarSizeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PerEach = table.Column<decimal>(type: "TEXT", nullable: false),
                    PerFootInstall = table.Column<decimal>(type: "TEXT", nullable: false),
                    PerEachInstall = table.Column<decimal>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricReinforcingPrices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetricReinforcingPrices");
        }
    }
}
