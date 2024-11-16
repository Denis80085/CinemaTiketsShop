using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.Models
{
    public class Producer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }

        [Required]
        public string FotoURL { get; set; } = string.Empty;

        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
