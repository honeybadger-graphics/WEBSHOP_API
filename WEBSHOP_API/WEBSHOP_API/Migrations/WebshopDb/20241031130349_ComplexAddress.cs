using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBSHOP_API.Migrations.WebshopDb
{
    /// <inheritdoc />
    public partial class ComplexAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserAddress",
                table: "UserDatas",
                newName: "UserAddress_Street");

            migrationBuilder.AddColumn<string>(
                name: "UserAddress_City",
                table: "UserDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAddress_HouseNumber",
                table: "UserDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAddress_PostCode",
                table: "UserDatas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAddress_City",
                table: "UserDatas");

            migrationBuilder.DropColumn(
                name: "UserAddress_HouseNumber",
                table: "UserDatas");

            migrationBuilder.DropColumn(
                name: "UserAddress_PostCode",
                table: "UserDatas");

            migrationBuilder.RenameColumn(
                name: "UserAddress_Street",
                table: "UserDatas",
                newName: "UserAddress");
        }
    }
}
