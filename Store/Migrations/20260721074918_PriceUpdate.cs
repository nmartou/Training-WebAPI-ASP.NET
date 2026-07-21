using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class PriceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Prices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "PriceID",
                keyValue: 1,
                column: "Currency",
                value: "USD");

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "PriceID",
                keyValue: 2,
                column: "Currency",
                value: "USD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Prices");
        }
    }
}
