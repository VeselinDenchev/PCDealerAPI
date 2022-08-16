using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.DbContext.Migrations
{
    public partial class Add_Comment_property_to_Review_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Reviews");
        }
    }
}
