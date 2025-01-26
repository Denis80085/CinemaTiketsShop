using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cinema Name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Descrption")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Logo Picture")]
        public string Logo { get; set; } = string.Empty;

        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
