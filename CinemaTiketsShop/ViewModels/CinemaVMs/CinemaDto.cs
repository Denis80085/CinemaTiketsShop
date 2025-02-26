using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.ViewModels.BaseAbstractVMs;
using System.ComponentModel.DataAnnotations;


namespace CinemaTiketsShop.ViewModels.CinemaVMs
{
    public class CinemaDto : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cinema Name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Descrption")]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Logo Picture")]
        public string LogoUrl { get; set; } = string.Empty;
        public Queue<ItemSelect?>? MoviesQueue { get; set; }
    }
}
