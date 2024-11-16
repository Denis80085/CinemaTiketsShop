using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace CinemaTiketsShop.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }

        [Required]
        public string FotoURL { get; set; } = string.Empty;

        //Relationships
        public List<Movie_Actor>? Movies_Actors { get; set; }
    }
}
