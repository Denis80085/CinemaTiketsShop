using CinemaTiketsShop.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.ViewModels.MovieVMs
{
    public class MovieDetailsViewModel
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
    }
}
