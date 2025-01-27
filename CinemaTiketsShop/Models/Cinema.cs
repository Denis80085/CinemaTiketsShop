using CinemaTiketsShop.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.Models
{
    public class Cinema : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cinema Name")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Descrption")]
        public string? Description { get; set; } 
        [Required]
        [Display(Name = "Logo Picture")]
        public  string? LogoUrl { get; set; } 

        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
