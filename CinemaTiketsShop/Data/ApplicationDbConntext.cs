using CinemaTiketsShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Data
{
    public class ApplicationDbConntext : DbContext
    {
        public ApplicationDbConntext(DbContextOptions options) : base(options)
        {
            
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


        }
    }
}
