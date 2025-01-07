using CinemaTiketsShop.Models;
using CinemaTiketsShop.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Data
{
    public class ApplicationDbConntext : DbContext
    {
        private readonly IPhotoService _photoService;

        public ApplicationDbConntext(DbContextOptions options,  IPhotoService photoService) : base(options)
        {
            _photoService = photoService;
        }

        public required DbSet<Actor> Actors { get; set; }
        public required DbSet<Producer> Producers { get; set; }
        public required DbSet<Cinema> Cinemas {  get; set; }
        public required DbSet<Movie> Movies { get; set; }
        public required DbSet<Movie_Actor> Movies_Actors { get; set; } //Join table

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Actor>(a => {
                a.HasData(
                   new Actor
                   {
                       Id = 1,
                       Name = "Leonardo DiCaprio",
                       Bio = "Popular actor. He was  filmed in Titanic, Shatered Island, Inception, Try catch me if you can, Wolf of the wool street and a lot more.",
                       FotoURL = "https://phantom-marca.unidadeditorial.es/525c725b581b2cb9476fb16e947a5e49/resize/660/f/webp/assets/multimedia/imagenes/2024/10/23/17296866914532.png"
                   },
                   new Actor 
                   { 
                       Id = 2,
                       Name = "Bred Pit",
                       Bio = "Popular actor. He had filmed in lot of popular movies.",
                       FotoURL = "https://image.stern.de/34287660/t/4O/v1/w1440/r1.7778/-/brad-pitt-cannes.jpg"
                   },
                   new Actor
                   {
                       Id = 3,
                       Name = "Johny Depp",
                       Bio = "Popular actor. He had filmed in lot of popular movies.",
                       FotoURL = "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/33623_v9_bd.jpg"
                   });
            });
            
            builder.Entity<Producer>(p => 
            {
                p.HasData(
                    new Producer { 
                        Id = 1,
                        Name = "Quentin Tarantino",
                        Bio = "My beloved producer. For his entire carrer, he has made such cool movies as: Inglorious Bastards, Kill Bill, Pulp Fiction etc.",
                        FotoURL = "https://cdn.britannica.com/02/156802-050-12ABFA13/Quentin-Tarantino.jpg"
                    },
                    new Producer 
                    {
                        Id = 2,
                        Name = "Martin Scorsese",
                        Bio = "Thery talented producer. His carear contains such films as Godfellas, Woolf of the wool street, Shutered island etc.",
                        FotoURL = "https://encrypted-tbn2.gstatic.com/licensed-image?q=tbn:ANd9GcT1T9q4leZMVWGx-_AFAwhe9jbRSevlm_y2Vi5F4MkCLgwUmNhSc8nddZPtY4vvJI1emvb7YJid1Ki3ESM"
                    },
                    new Producer
                    {
                        Id = 3,
                        Name = "Espen Sandberg",
                        Bio = "producer of pirates of the caribbean",
                        FotoURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfw8TbUDvrqSCEruiCs44JJeqRV5q4lw1picG3KgkfpVlO-2rpCv_2MUj5IX18FkeQsik1wzLaed1W2CwCzuGIYA"
                    }
                );
            });

            builder.Entity<Movie>(m => 
            {
                m.HasOne(m => m.Cinema)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CinemaId);  //One To Many
                
                m.HasOne(m => m.Producer)
                .WithMany(p => p.Movies)
                .HasForeignKey(m => m.ProducerId); //One To Many
            });

            builder.Entity<Movie_Actor>(ma =>
            {
                ma.HasOne(ma => ma.Movie)
                .WithMany(m => m.Movies_Actors)
                .HasForeignKey(ma =>  ma.MovieId);

                ma.HasOne(ma => ma.Actor)
                .WithMany(a => a.Movies_Actors)
                .HasForeignKey(ma => ma.ActorId); //Many to Many

                ma.HasKey(ma => new {ma.MovieId, ma.ActorId});
            });

            //seeding Cinemas
            builder.Entity<Cinema>().HasData(
                new Cinema 
                {
                    Id = 1,
                    Name = "Disel Kino",
                    Logo = "https://t3.ftcdn.net/jpg/01/25/57/92/360_F_125579217_HL9SYmJR8KzVZ5Jfddr4BPyD3QxSSHtZ.jpg",
                    Description = "Big cinema. Evrey week new realeases. Suports new talented producers"
                },
                new Cinema 
                {
                    Id=2,
                    Name = "Hookie Cinema",
                    Logo = "https://static.vecteezy.com/system/resources/previews/028/190/887/non_2x/cinema-logo-vector.jpg",
                    Description = "Cosy Cinema, all brand new movies. Anime night evrey monday"
                }
                    
            );

            //Seeding Movies
            builder.Entity<Movie>()
                .HasData(
                    new Movie 
                    {
                        Id = 1,
                        Name = "Unglorious Bastards",
                        ProducerId = 1,
                        Description = "Other history of the WW2",
                        Logo = "https://de.web.img3.acsta.net/medias/nmedia/18/71/58/48/19138855.jpg",
                        StartDate = DateTime.Parse("18.11.2024"),
                        EndDate = DateTime.Parse("26.11.2024"),
                        Price = 18.4,
                        CinemaId = 2,
                        Category = Enums.MovieCategory.Action   
                    },
                    new Movie 
                    {
                        Id = 2,
                        Name = "Wolf of Wall street",
                        ProducerId = 2,
                        Description = "The life of a guy, who made a dirty buisnes",
                        Logo = "https://de.web.img2.acsta.net/pictures/210/613/21061365_20131127123712997.jpg",
                        StartDate = DateTime.Parse("18.11.2024"),
                        EndDate = DateTime.Parse("26.11.2024"),
                        Price = 18.4,
                        CinemaId = 1,
                        Category = Enums.MovieCategory.Criminal
                    },
                    new Movie
                    {
                        Id = 3,
                        Name = "Wolf of Wall street",
                        ProducerId = 2,
                        Description = "The life of a guy, who made a dirty buisnes",
                        Logo = "https://de.web.img2.acsta.net/pictures/210/613/21061365_20131127123712997.jpg",
                        StartDate = DateTime.Parse("18.11.2024"),
                        EndDate = DateTime.Parse("28.11.2024"),
                        Price = 18.4,
                        CinemaId = 2,
                        Category = Enums.MovieCategory.Criminal
                    },
                    new Movie
                    {
                        Id = 4,
                        Name = "Pirates of Caribbean",
                        ProducerId = 3,
                        Description = "Disney movie about pirates",
                        Logo = "https://m.media-amazon.com/images/M/MV5BMjE5MjkwODI3Nl5BMl5BanBnXkFtZTcwNjcwMDk4NA@@._V1_.jpg",
                        StartDate = DateTime.Parse("30.11.2024"),
                        EndDate = DateTime.Parse("01.02.2025"),
                        Price = 20.0,
                        CinemaId = 1,
                        Category = Enums.MovieCategory.Action
                    }
                );

            //Seeding Movies_Actors

            builder.Entity<Movie_Actor>()
                .HasData(
                    new Movie_Actor
                    {
                        ActorId = 1,
                        MovieId = 2
                    },
                    new Movie_Actor
                    {
                        ActorId = 2,
                        MovieId = 1
                    },
                    new Movie_Actor
                    {
                        ActorId = 3,
                        MovieId = 4
                    }
                );

        }
    }
}
