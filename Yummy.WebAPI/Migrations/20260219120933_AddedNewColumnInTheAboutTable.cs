using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yummy.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewColumnInTheAboutTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListDescription1",
                table: "Abouts",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListDescription2",
                table: "Abouts",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListDescription3",
                table: "Abouts",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListDescription1",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "ListDescription2",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "ListDescription3",
                table: "Abouts");
        }
    }
}
