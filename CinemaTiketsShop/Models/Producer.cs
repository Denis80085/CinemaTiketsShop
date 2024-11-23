using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.Models
{
    public class Producer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Full Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Biography")]
        public string? Bio { get; set; }

        [Required]
        [Display(Name = "Foto")]
        public string FotoURL { get; set; } = string.Empty;

        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
