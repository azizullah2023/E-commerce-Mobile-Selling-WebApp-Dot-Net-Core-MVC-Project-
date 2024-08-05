using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class tiger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsedYears = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormMobileSellIts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    TAConditionalDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TAAccessories = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormMobileSellIts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormMobileSellIts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileSellingID = table.Column<int>(type: "int", nullable: false),
                    ComntConetent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CraetedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MobileSellId = table.Column<int>(type: "int", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: true),
                    isLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_FormMobileSellIts_MobileSellingID",
                        column: x => x.MobileSellingID,
                        principalTable: "FormMobileSellIts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "MobileModel", "UsedYears", "brand" },
                values: new object[,]
                {
                    { 1, "Galaxy S20", "1", "Samsung" },
                    { 2, "iPhone 12", "0.5", "Apple" },
                    { 3, "Xperia 5", "2", "Sony" },
                    { 4, "Pixel 5", "0.5", "Google" },
                    { 5, "8T", "1.5", "OnePlus" },
                    { 6, "Mi 10T Pro", "0.8", "Xiaomi" },
                    { 7, "P40 Pro", "1.2", "Huawei" },
                    { 8, "Velvet", "2.5", "LG" },
                    { 9, "Moto G Power", "0.3", "Motorola" },
                    { 10, "8.3 5G", "1.8", "Nokia" },
                    { 11, "Zenfone 7 Pro", "0.7", "Asus" },
                    { 12, "KEY2", "2.3", "BlackBerry" },
                    { 13, "U12 Plus", "3.0", "HTC" },
                    { 14, "Find X2 Pro", "0.9", "Oppo" },
                    { 15, "X7 Pro", "0.6", "Realme" },
                    { 16, "X50 Pro", "1.7", "Vivo" },
                    { 17, "Legion Phone Duel", "0.4", "Lenovo" },
                    { 18, "Axon 20 5G", "1.4", "ZTE" },
                    { 19, "X3 NFC", "0.2", "Poco" },
                    { 20, "10 Pro", "2.1", "TCL" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MobileSellingID",
                table: "Comments",
                column: "MobileSellingID");

            migrationBuilder.CreateIndex(
                name: "IX_FormMobileSellIts_BrandId",
                table: "FormMobileSellIts",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FormMobileSellIts");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
