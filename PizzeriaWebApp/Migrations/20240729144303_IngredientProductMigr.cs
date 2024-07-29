using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class IngredientProductMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Ingredients_IngredientId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IngredientId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "IngredientProduct",
                columns: table => new
                {
                    IngredientsIngredientId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientProduct", x => new { x.IngredientsIngredientId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_IngredientProduct_Ingredients_IngredientsIngredientId",
                        column: x => x.IngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientProduct_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientProduct_ProductsProductId",
                table: "IngredientProduct",
                column: "ProductsProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientProduct");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_IngredientId",
                table: "Products",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Ingredients_IngredientId",
                table: "Products",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId");
        }
    }
}
