using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salesandInventoryforSlowItemsShops.Migrations
{
    /// <inheritdoc />
    public partial class modifiedproductandinventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerchesPricePerUnit",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "SalesPricePerUnit",
                table: "Inventories");

            migrationBuilder.AddColumn<double>(
                name: "PurchasePrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalePrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "PerchesPricePerUnit",
                table: "Inventories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalesPricePerUnit",
                table: "Inventories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
