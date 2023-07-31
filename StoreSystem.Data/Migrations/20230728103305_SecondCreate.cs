using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_UserId",
                table: "Sales",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_UserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sales");
        }
    }
}
