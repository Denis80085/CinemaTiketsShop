using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTiketsShop.Migrations
{
    /// <inheritdoc />
    public partial class Pirates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "FotoURL", "Name" },
                values: new object[] { 3, "Popular actor. He had filmed in lot of popular movies.", "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/33623_v9_bd.jpg", "Johny Depp" });

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Logo",
                value: "https://t3.ftcdn.net/jpg/01/25/57/92/360_F_125579217_HL9SYmJR8KzVZ5Jfddr4BPyD3QxSSHtZ.jpg");

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Logo",
                value: "https://static.vecteezy.com/system/resources/previews/028/190/887/non_2x/cinema-logo-vector.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Logo", "Name" },
                values: new object[] { "https://de.web.img2.acsta.net/pictures/210/613/21061365_20131127123712997.jpg", "Wolf of Wall street" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Category", "CinemaId", "Description", "EndDate", "Logo", "Name", "Price", "ProducerId", "StartDate" },
                values: new object[] { 3, 7, 1, "The life of a guy, who made a dirty buisnes", new DateTime(2024, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://de.web.img2.acsta.net/pictures/210/613/21061365_20131127123712997.jpg", "Wolf of Wall street", 18.399999999999999, 2, new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Bio", "FotoURL", "Name" },
                values: new object[] { 3, "producer of pirates of the caribbean", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfw8TbUDvrqSCEruiCs44JJeqRV5q4lw1picG3KgkfpVlO-2rpCv_2MUj5IX18FkeQsik1wzLaed1W2CwCzuGIYA", "Espen Sandberg" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Category", "CinemaId", "Description", "EndDate", "Logo", "Name", "Price", "ProducerId", "StartDate" },
                values: new object[] { 4, 5, 1, "Disney movie about pirates", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://m.media-amazon.com/images/M/MV5BMjE5MjkwODI3Nl5BMl5BanBnXkFtZTcwNjcwMDk4NA@@._V1_.jpg", "Pirates of Caribbean", 20.0, 3, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Movies_Actors",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[] { 3, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies_Actors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Logo",
                value: "logo url");

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Logo",
                value: "logo URL");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Logo", "Name" },
                values: new object[] { "https://upload.wikimedia.org/wikipedia/en/7/7b/Goodfellas.jpg", "Wolf of the Wall street" });
        }
    }
}
