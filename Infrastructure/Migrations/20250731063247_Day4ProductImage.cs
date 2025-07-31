using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Day4ProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Basket_BasketId1",
                table: "BasketItem");

            migrationBuilder.DropIndex(
                name: "IX_BasketItem_BasketId1",
                table: "BasketItem");

            migrationBuilder.DropColumn(
                name: "BasketId1",
                table: "BasketItem");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "ProductImage");

            migrationBuilder.AddColumn<int>(
                name: "BasketId1",
                table: "BasketItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_BasketId1",
                table: "BasketItem",
                column: "BasketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Basket_BasketId1",
                table: "BasketItem",
                column: "BasketId1",
                principalTable: "Basket",
                principalColumn: "Id");
        }
    }
}
