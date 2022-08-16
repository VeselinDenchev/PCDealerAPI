using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.DbContext.Migrations
{
    public partial class Move_Category_property_from_Product_to_Model_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Models",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Models_CategoryId",
                table: "Models",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Categories_CategoryId",
                table: "Models",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Categories_CategoryId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_CategoryId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Models");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
