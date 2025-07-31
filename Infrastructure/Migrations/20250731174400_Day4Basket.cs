using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Day4Basket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Brand_BrandId",
                table: "BasketItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Category_CategoryId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_BasketItem_BrandId",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_BasketItem_CategoryId",
                table: "BasketItem");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "BasketItem");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BasketItem");

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "BasketItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BasketItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_BrandId",
                table: "BasketItem",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_CategoryId",
                table: "BasketItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Brand_BrandId",
                table: "BasketItem",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Category_CategoryId",
                table: "BasketItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
