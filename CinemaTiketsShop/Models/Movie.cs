
using CinemaTiketsShop.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTiketsShop.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Film name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Film image")]
        public string Logo { get; set; } = string.Empty;
        [Required]
        public MovieCategory Category { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public double Price { get; set; }

        //Ralationships
        public int CinemaId { get; set; }

        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        public int ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        public Producer? Producer { get; set; }

        public List<Movie_Actor>? Movies_Actors { get; set; }
    }
}
