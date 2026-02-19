using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yummy.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewColumnInTheCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataTarget",
                table: "Categories",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataTarget",
                table: "Categories");
        }
    }
}
