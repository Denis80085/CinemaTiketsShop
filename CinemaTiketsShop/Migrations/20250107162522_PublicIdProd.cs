using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTiketsShop.Migrations
{
    /// <inheritdoc />
    public partial class PublicIdProd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Producers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Producers");
        }
    }
}
