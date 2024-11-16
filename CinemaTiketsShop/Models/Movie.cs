
using CinemaTiketsShop.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTiketsShop.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Logo { get; set; } = string.Empty;
        [Required]
        public MovieCategory Category { get; set; }

        //Ralationships
        public int CinemaId { get; set; }

        [ForeignKey("CinemaId")]
        public required Cinema Cinema { get; set; }

        public int ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        public required Producer Producer { get; set; }

        public List<Movie_Actor>? Movies_Actors { get; set; }
    }
}
