using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaTiketsShop.Migrations
{
    /// <inheritdoc />
    public partial class CinemaSessions : Migration
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
                    Name = table.Column<string>(type: "nvarchar(270)", maxLength: 270, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(270)", maxLength: 270, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cinema_Halls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Call = table.Column<int>(type: "int", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinema_Halls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cinema_Halls_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CinemaId = table.Column<int>(type: "int", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Movie_Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Starts = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ends = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Sessions_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Movies_Actors",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies_Actors", x => new { x.MovieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_Movies_Actors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_Actors_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CinemaHall_MovieSessions",
                columns: table => new
                {
                    CinemaHallId = table.Column<int>(type: "int", nullable: false),
                    MovieSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHall_MovieSessions", x => new { x.CinemaHallId, x.MovieSessionId });
                    table.ForeignKey(
                        name: "FK_CinemaHall_MovieSessions_Cinema_Halls_CinemaHallId",
                        column: x => x.CinemaHallId,
                        principalTable: "Cinema_Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CinemaHall_MovieSessions_Movie_Sessions_MovieSessionId",
                        column: x => x.MovieSessionId,
                        principalTable: "Movie_Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "FotoURL", "Name", "PublicId" },
                values: new object[,]
                {
                    { 1, "Popular actor. He was  filmed in Titanic, Shatered Island, Inception, Try catch me if you can, Wolf of the wool street and a lot more.", "https://phantom-marca.unidadeditorial.es/525c725b581b2cb9476fb16e947a5e49/resize/660/f/webp/assets/multimedia/imagenes/2024/10/23/17296866914532.png", "Leonardo DiCaprio", null },
                    { 2, "Popular actor. He had filmed in lot of popular movies.", "https://image.stern.de/34287660/t/4O/v1/w1440/r1.7778/-/brad-pitt-cannes.jpg", "Bred Pit", null },
                    { 3, "Popular actor. He had filmed in lot of popular movies.", "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/33623_v9_bd.jpg", "Johny Depp", null }
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Description", "LogoUrl", "Name", "PublicId" },
                values: new object[,]
                {
                    { 1, "Big cinema. Evrey week new realeases. Suports new talented producers", "https://t3.ftcdn.net/jpg/01/25/57/92/360_F_125579217_HL9SYmJR8KzVZ5Jfddr4BPyD3QxSSHtZ.jpg", "Disel Kino", null },
                    { 2, "Cosy Cinema, all brand new movies. Anime night evrey monday", "https://static.vecteezy.com/system/resources/previews/028/190/887/non_2x/cinema-logo-vector.jpg", "Hookie Cinema", null }
                });

            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Bio", "FotoURL", "Name", "PublicId" },
                values: new object[,]
                {
                    { 1, "My beloved producer. For his entire carrer, he has made such cool movies as: Inglorious Bastards, Kill Bill, Pulp Fiction etc.", "https://cdn.britannica.com/02/156802-050-12ABFA13/Quentin-Tarantino.jpg", "Quentin Tarantino", null },
                    { 2, "Thery talented producer. His carear contains such films as Godfellas, Woolf of the wool street, Shutered island etc.", "https://encrypted-tbn2.gstatic.com/licensed-image?q=tbn:ANd9GcT1T9q4leZMVWGx-_AFAwhe9jbRSevlm_y2Vi5F4MkCLgwUmNhSc8nddZPtY4vvJI1emvb7YJid1Ki3ESM", "Martin Scorsese", null },
                    { 3, "producer of pirates of the caribbean", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfw8TbUDvrqSCEruiCs44JJeqRV5q4lw1picG3KgkfpVlO-2rpCv_2MUj5IX18FkeQsik1wzLaed1W2CwCzuGIYA", "Espen Sandberg", null }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Category", "CinemaId", "Description", "EndDate", "Logo", "Name", "Price", "ProducerId", "PublicId", "StartDate" },
                values: new object[,]
                {
                    { 1, 5, 2, "Other history of the WW2", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://de.web.img3.acsta.net/medias/nmedia/18/71/58/48/19138855.jpg", "Unglorious Bastards", 18.399999999999999, 1, null, new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 7, 1, "The life of a guy, who made a dirty buisnes", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://de.web.img2.acsta.net/pictures/210/613/21061365_20131127123712997.jpg", "Wolf of Wall street", 18.399999999999999, 2, null, new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 7, 2, "The life of a guy, who made a dirty buisnes", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://de.web.img2.acsta.net/pictures/210/613/21061365_20131127123712997.jpg", "Wolf of Wall street", 18.399999999999999, 2, null, new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 5, 1, "Disney movie about pirates", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://m.media-amazon.com/images/M/MV5BMjE5MjkwODI3Nl5BMl5BanBnXkFtZTcwNjcwMDk4NA@@._V1_.jpg", "Pirates of Caribbean", 20.0, 3, null, new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Movies_Actors",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cinema_Halls_CinemaId",
                table: "Cinema_Halls",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHall_MovieSessions_MovieSessionId",
                table: "CinemaHall_MovieSessions",
                column: "MovieSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Sessions_MovieId",
                table: "Movie_Sessions",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CinemaId",
                table: "Movies",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ProducerId",
                table: "Movies",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Actors_ActorId",
                table: "Movies_Actors",
                column: "ActorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CinemaHall_MovieSessions");

            migrationBuilder.DropTable(
                name: "Movies_Actors");

            migrationBuilder.DropTable(
                name: "Cinema_Halls");

            migrationBuilder.DropTable(
                name: "Movie_Sessions");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Cinemas");

            migrationBuilder.DropTable(
                name: "Producers");
        }
    }
}
