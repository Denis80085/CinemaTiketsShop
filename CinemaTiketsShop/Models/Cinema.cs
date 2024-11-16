using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Logo { get; set; } = string.Empty;

        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
