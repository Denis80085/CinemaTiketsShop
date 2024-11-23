using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace CinemaTiketsShop.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Full name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Biography")]
        public string? Bio { get; set; }

        [Required]
        [Display(Name = "Foto")]
        public string FotoURL { get; set; } = string.Empty;

        //Relationships
        public List<Movie_Actor>? Movies_Actors { get; set; }
    }
}
