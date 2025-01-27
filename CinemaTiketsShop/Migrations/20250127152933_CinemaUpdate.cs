using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTiketsShop.Migrations
{
    /// <inheritdoc />
    public partial class CinemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Cinemas",
                newName: "LogoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "Cinemas",
                newName: "Logo");
        }
    }
}
