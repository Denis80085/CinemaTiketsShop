
using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Data.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.Models
{
    public class Movie : IEntityBase
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

        [AllowNull]
        public string? PublicId { get; set; }

        //Ralationships
        public int CinemaId { get; set; }

        [ForeignKey("CinemaId")]
        [Required]
        [JsonIgnore]
        public Cinema? Cinema { get; set; }

        [AllowNull]
        public int? ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        [JsonIgnore]
        public Producer? Producer { get; set; }

        [JsonIgnore]
        public List<Movie_Actor>? Movies_Actors { get; set; }
    }
}
