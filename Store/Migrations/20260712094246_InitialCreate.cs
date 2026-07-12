using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    PriceID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<decimal>(type: "money", nullable: false),
                    Discount = table.Column<float>(type: "REAL", nullable: false),
                    DiscountEndDate = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.PriceID);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    CommandID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalQuanity = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    SellerID = table.Column<int>(type: "INTEGER", nullable: false),
                    BuyerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.CommandID);
                    table.ForeignKey(
                        name: "FK_Commands_Persons_BuyerID",
                        column: x => x.BuyerID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commands_Persons_SellerID",
                        column: x => x.SellerID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Prices_PriceID",
                        column: x => x.PriceID,
                        principalTable: "Prices",
                        principalColumn: "PriceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCommands",
                columns: table => new
                {
                    ProductCommandID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommandID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCommands", x => x.ProductCommandID);
                    table.ForeignKey(
                        name: "FK_ProductCommands_Commands_CommandID",
                        column: x => x.CommandID,
                        principalTable: "Commands",
                        principalColumn: "CommandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCommands_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonID", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateOnly(2000, 10, 2), "Nicolas", "Martou" },
                    { 2, new DateOnly(1971, 6, 28), "Elon", "Musk" },
                    { 3, new DateOnly(1955, 10, 28), "Bill", "Gates" }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "PriceID", "Discount", "DiscountEndDate", "Value" },
                values: new object[,]
                {
                    { 1, 0f, null, 34.99m },
                    { 2, 15f, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.99m }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "CommandID", "BuyerID", "IsPaid", "SellerID", "TotalPrice", "TotalQuanity" },
                values: new object[,]
                {
                    { 1, 3, true, 1, 10m, 8 },
                    { 2, 2, true, 1, 10m, 3 },
                    { 3, 3, false, 1, 0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Brand", "Description", "Name", "PriceID", "Stock" },
                values: new object[,]
                {
                    { 1, "Micosoft", "This is our latest white hat which is very useful against the sun.", "White Hat", 1, 42 },
                    { 2, "MacroHard", "This object would fit perfectly with a white hat.", "T-shirt", 2, 26 }
                });

            migrationBuilder.InsertData(
                table: "ProductCommands",
                columns: new[] { "ProductCommandID", "CommandID", "ProductID", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 4 },
                    { 2, 1, 2, 4 },
                    { 3, 2, 1, 1 },
                    { 4, 2, 2, 2 },
                    { 5, 3, 1, 1 },
                    { 6, 3, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_BuyerID",
                table: "Commands",
                column: "BuyerID");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_SellerID",
                table: "Commands",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCommands_CommandID",
                table: "ProductCommands",
                column: "CommandID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCommands_ProductID",
                table: "ProductCommands",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PriceID",
                table: "Products",
                column: "PriceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCommands");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
