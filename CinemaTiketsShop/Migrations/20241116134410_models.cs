using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaTiketsShop.Migrations
{
    /// <inheritdoc />
    public partial class models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "FotoURL", "Name" },
                values: new object[,]
                {
                    { 1, "Popular actor. He was  filmed in Titanic, Shatered Island, Inception, Try catch me if you can, Wolf of the wool street and a lot more.", "https://phantom-marca.unidadeditorial.es/525c725b581b2cb9476fb16e947a5e49/resize/660/f/webp/assets/multimedia/imagenes/2024/10/23/17296866914532.png", "Leonardo DiCaprio" },
                    { 2, "Popular actor. He had filmed in lot of popular movies.", "https://image.stern.de/34287660/t/4O/v1/w1440/r1.7778/-/brad-pitt-cannes.jpg", "Bred Pit" }
                });

            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Bio", "FotoURL", "Name" },
                values: new object[,]
                {
                    { 1, "My beloved producer. For his entire carrer, he has made such cool movies as: Inglorious Bastards, Kill Bill, Pulp Fiction etc.", "https://cdn.britannica.com/02/156802-050-12ABFA13/Quentin-Tarantino.jpg", "Quentin Tarantino" },
                    { 2, "Thery talented producer. His carear contains such films as Godfellas, Woolf of the wool street, Shutered island etc.", "https://cdn.britannica.com/02/156802-050-12ABFA13/Quentin-Tarantino.jpg", "Martin Scorsese" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Producers");
        }
    }
}
