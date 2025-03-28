﻿// <auto-generated />
using System;
using CinemaTiketsShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaTiketsShop.Migrations
{
    [DbContext(typeof(ApplicationDbConntext))]
    [Migration("20250310215118_gitf")]
    partial class gitf
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaTiketsShop.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FotoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(270)
                        .HasColumnType("nvarchar(270)");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "Popular actor. He was  filmed in Titanic, Shatered Island, Inception, Try catch me if you can, Wolf of the wool street and a lot more.",
                            FotoURL = "https://phantom-marca.unidadeditorial.es/525c725b581b2cb9476fb16e947a5e49/resize/660/f/webp/assets/multimedia/imagenes/2024/10/23/17296866914532.png",
                            Name = "Leonardo DiCaprio"
                        },
                        new
                        {
                            Id = 2,
                            Bio = "Popular actor. He had filmed in lot of popular movies.",
                            FotoURL = "https://image.stern.de/34287660/t/4O/v1/w1440/r1.7778/-/brad-pitt-cannes.jpg",
                            Name = "Bred Pit"
                        },
                        new
                        {
                            Id = 3,
                            Bio = "Popular actor. He had filmed in lot of popular movies.",
                            FotoURL = "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/33623_v9_bd.jpg",
                            Name = "Johny Depp"
                        });
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Big cinema. Evrey week new realeases. Suports new talented producers",
                            LogoUrl = "https://t3.ftcdn.net/jpg/01/25/57/92/360_F_125579217_HL9SYmJR8KzVZ5Jfddr4BPyD3QxSSHtZ.jpg",
                            Name = "Disel Kino"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Cosy Cinema, all brand new movies. Anime night evrey monday",
                            LogoUrl = "https://static.vecteezy.com/system/resources/previews/028/190/887/non_2x/cinema-logo-vector.jpg",
                            Name = "Hookie Cinema"
                        });
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.CinemaHall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Call")
                        .HasColumnType("int");

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.ToTable("Cinema_Halls");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.CinemaHall_MovieSession", b =>
                {
                    b.Property<int>("CinemaHallId")
                        .HasColumnType("int");

                    b.Property<int>("MovieSessionId")
                        .HasColumnType("int");

                    b.HasKey("CinemaHallId", "MovieSessionId");

                    b.HasIndex("MovieSessionId");

                    b.ToTable("CinemaHall_MovieSessions");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("ProducerId")
                        .HasColumnType("int");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("ProducerId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 5,
                            CinemaId = 2,
                            Description = "Other history of the WW2",
                            EndDate = new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Logo = "https://de.web.img3.acsta.net/medias/nmedia/18/71/58/48/19138855.jpg",
                            Name = "Unglorious Bastards",
                            Price = 18.399999999999999,
                            ProducerId = 1,
                            StartDate = new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Category = 7,
                            CinemaId = 1,
                            Description = "The life of a guy, who made a dirty buisnes",
                            EndDate = new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Logo = "https://de.web.img2.acsta.net/pictures/210/613/21061365_20131127123712997.jpg",
                            Name = "Wolf of Wall street",
                            Price = 18.399999999999999,
                            ProducerId = 2,
                            StartDate = new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Category = 7,
                            CinemaId = 2,
                            Description = "The life of a guy, who made a dirty buisnes",
                            EndDate = new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Logo = "https://de.web.img2.acsta.net/pictures/210/613/21061365_20131127123712997.jpg",
                            Name = "Wolf of Wall street",
                            Price = 18.399999999999999,
                            ProducerId = 2,
                            StartDate = new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Category = 5,
                            CinemaId = 1,
                            Description = "Disney movie about pirates",
                            EndDate = new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Logo = "https://m.media-amazon.com/images/M/MV5BMjE5MjkwODI3Nl5BMl5BanBnXkFtZTcwNjcwMDk4NA@@._V1_.jpg",
                            Name = "Pirates of Caribbean",
                            Price = 20.0,
                            ProducerId = 3,
                            StartDate = new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.MovieSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Ends")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Starts")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Movie_Sessions");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Movie_Actor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("Movies_Actors");

                    b.HasData(
                        new
                        {
                            MovieId = 2,
                            ActorId = 1
                        },
                        new
                        {
                            MovieId = 1,
                            ActorId = 2
                        },
                        new
                        {
                            MovieId = 4,
                            ActorId = 3
                        });
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FotoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(270)
                        .HasColumnType("nvarchar(270)");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Producers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "My beloved producer. For his entire carrer, he has made such cool movies as: Inglorious Bastards, Kill Bill, Pulp Fiction etc.",
                            FotoURL = "https://cdn.britannica.com/02/156802-050-12ABFA13/Quentin-Tarantino.jpg",
                            Name = "Quentin Tarantino"
                        },
                        new
                        {
                            Id = 2,
                            Bio = "Thery talented producer. His carear contains such films as Godfellas, Woolf of the wool street, Shutered island etc.",
                            FotoURL = "https://encrypted-tbn2.gstatic.com/licensed-image?q=tbn:ANd9GcT1T9q4leZMVWGx-_AFAwhe9jbRSevlm_y2Vi5F4MkCLgwUmNhSc8nddZPtY4vvJI1emvb7YJid1Ki3ESM",
                            Name = "Martin Scorsese"
                        },
                        new
                        {
                            Id = 3,
                            Bio = "producer of pirates of the caribbean",
                            FotoURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfw8TbUDvrqSCEruiCs44JJeqRV5q4lw1picG3KgkfpVlO-2rpCv_2MUj5IX18FkeQsik1wzLaed1W2CwCzuGIYA",
                            Name = "Espen Sandberg"
                        });
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.CinemaHall", b =>
                {
                    b.HasOne("CinemaTiketsShop.Models.Cinema", "Cinema")
                        .WithMany("Halls")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.CinemaHall_MovieSession", b =>
                {
                    b.HasOne("CinemaTiketsShop.Models.CinemaHall", "CinemaHall")
                        .WithMany("CinemaHall_MovieSessions")
                        .HasForeignKey("CinemaHallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaTiketsShop.Models.MovieSession", "MovieSession")
                        .WithMany("CinemaHall_MovieSessions")
                        .HasForeignKey("MovieSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CinemaHall");

                    b.Navigation("MovieSession");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Movie", b =>
                {
                    b.HasOne("CinemaTiketsShop.Models.Cinema", "Cinema")
                        .WithMany("Movies")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaTiketsShop.Models.Producer", "Producer")
                        .WithMany("Movies")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Cinema");

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.MovieSession", b =>
                {
                    b.HasOne("CinemaTiketsShop.Models.Movie", "Movie")
                        .WithMany("MovieSessions")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Movie_Actor", b =>
                {
                    b.HasOne("CinemaTiketsShop.Models.Actor", "Actor")
                        .WithMany("Movies_Actors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaTiketsShop.Models.Movie", "Movie")
                        .WithMany("Movies_Actors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Actor", b =>
                {
                    b.Navigation("Movies_Actors");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Cinema", b =>
                {
                    b.Navigation("Halls");

                    b.Navigation("Movies");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.CinemaHall", b =>
                {
                    b.Navigation("CinemaHall_MovieSessions");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Movie", b =>
                {
                    b.Navigation("MovieSessions");

                    b.Navigation("Movies_Actors");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.MovieSession", b =>
                {
                    b.Navigation("CinemaHall_MovieSessions");
                });

            modelBuilder.Entity("CinemaTiketsShop.Models.Producer", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
