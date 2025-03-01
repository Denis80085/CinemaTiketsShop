using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTiketsShop.Models
{
    public class CinemaHall
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Number { get; set; }

        public int Row { get; set; }
        public int Call { get; set; }

        [Required]
        public int CinemaId { get; set; }

        [ForeignKey(nameof(CinemaId))]
        public required Cinema Cinema { get; set; }

        public List<CinemaHall_MovieSession> CinemaHall_MovieSessions { get; set; }
    }
}
