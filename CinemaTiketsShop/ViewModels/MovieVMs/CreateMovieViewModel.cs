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
        override public string? Bio { get; set; } = string.Empty; 
        [Required(ErrorMessage = "Plese select a category")]
        public MovieCategory Category { get; set; }

        [Required(ErrorMessage = "Plese add a start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Plese add a end date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Plese write the price")]
        public double Price { get; set; }

        //Ralationships
        [Required(ErrorMessage = "Plese select a cinema")]
        public int CinemaId { get; set; }

        [Required(ErrorMessage = "Plese select a producer")]
        [DisallowNull]
        public int? ProducerId { get; set; }

        [Required(ErrorMessage = "Plese select actors")]
        public List<int> SelActors { get; set; } = new List<int>();

    }
}
