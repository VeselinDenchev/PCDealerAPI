using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.DbContext.Migrations
{
    public partial class Move_Brand_property_of_Product_to_Model_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadedFile_Products_ProductId",
                table: "UploadedFile");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadedFile",
                table: "UploadedFile");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UploadedFile");

            migrationBuilder.RenameTable(
                name: "UploadedFile",
                newName: "ImageFiles");

            migrationBuilder.RenameIndex(
                name: "IX_UploadedFile_ProductId",
                table: "ImageFiles",
                newName: "IX_ImageFiles_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "BrandId",
                table: "Models",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageFiles",
                table: "ImageFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_Products_ProductId",
                table: "ImageFiles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_Products_ProductId",
                table: "ImageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageFiles",
                table: "ImageFiles");

            migrationBuilder.RenameTable(
                name: "ImageFiles",
                newName: "UploadedFile");

            migrationBuilder.RenameIndex(
                name: "IX_ImageFiles_ProductId",
                table: "UploadedFile",
                newName: "IX_UploadedFile_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "BrandId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "BrandId",
                table: "Models",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UploadedFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadedFile",
                table: "UploadedFile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedFile_Products_ProductId",
                table: "UploadedFile",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
