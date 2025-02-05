using CinemaTiketsShop.Data.Enums;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.BaseAbstractVMs;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.ViewModels.MovieVMs
{
    public class CreateMovieViewModel : CreateBaseViewModel
    {
        [Display(Name = "Description")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Description must contain at least 2 and maximum 500 chars")]
        override public string? Bio { get; set; }
        [Required]
        public MovieCategory Category { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public double Price { get; set; }

        //Ralationships
        [Required]
        public int CinemaId { get; set; }

        [Required]
        public int ProducerId { get; set; }

        public LinkedList<Actor>? Actors { get; set; }

    }
}
