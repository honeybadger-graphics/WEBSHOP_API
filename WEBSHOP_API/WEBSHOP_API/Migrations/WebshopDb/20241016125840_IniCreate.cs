using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBSHOP_API.Migrations.WebshopDb
{
    /// <inheritdoc />
    public partial class IniCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductsId = table.Column<string>(type: "TEXT", nullable: true),
                    ProductsCounts = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: true),
                    ProductDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ProductCategory = table.Column<string>(type: "TEXT", nullable: true),
                    ProductImage = table.Column<string>(type: "TEXT", nullable: true),
                    ProductPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductBasePrice = table.Column<int>(type: "INTEGER", nullable: false),
                    IsProductPromoted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsProductOnSale = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductStocks = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });

            migrationBuilder.CreateTable(
                name: "UserDatas",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    UserNameTitles = table.Column<string>(type: "TEXT", nullable: true),
                    UserFirstName = table.Column<string>(type: "TEXT", nullable: true),
                    UserLastName = table.Column<string>(type: "TEXT", nullable: true),
                    UserAddress = table.Column<string>(type: "TEXT", nullable: true),
                    UserLastPurchaseCategory = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDatas", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "UserDatas");
        }
    }
}
