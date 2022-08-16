using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.DbContext.Migrations
{
    public partial class Remove_Specification_and_SpecificationType_entities_and_add_specification_properties_in_Product_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "SpecificationTypes");

            migrationBuilder.AddColumn<string>(
                name: "Display",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GPU",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Processor",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ram",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Storage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Display",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GPU",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Processor",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Ram",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Storage",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "SpecificationTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Specifications_SpecificationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SpecificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_ProductId",
                table: "Specifications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_TypeId",
                table: "Specifications",
                column: "TypeId");
        }
    }
}
