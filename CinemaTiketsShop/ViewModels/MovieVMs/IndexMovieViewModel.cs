using CinemaTiketsShop.Data.Enums;
using CinemaTiketsShop.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CinemaTiketsShop.Data.Base;

namespace CinemaTiketsShop.ViewModels.MovieVMs
{
    public class IndexMovieViewModel : IEntityBase
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

        [Required]
        public required string CinemaName { get; set; }

        [AllowNull]
        public int? ProducerId { get; set; }

        public string? ProducerName { get; set; }

    }
}
