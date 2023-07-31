using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class MyPcMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyBrandWiseSalesReports",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    TotalSales = table.Column<int>(type: "int", nullable: false),
                    TotalProfitLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_MonthlyBrandWiseSalesReports_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlySalesReportItems",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    TotalSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalProfitLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyBrandWiseSalesReports_BrandId",
                table: "MonthlyBrandWiseSalesReports",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyBrandWiseSalesReports");

            migrationBuilder.DropTable(
                name: "MonthlySalesReportItems");
        }
    }
}
