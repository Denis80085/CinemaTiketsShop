using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTiketsShop.Models
{
    public class MovieSession
    {
        [Key]
        public int Id { get; set; }

        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }

        public List<CinemaHall_MovieSession> CinemaHall_MovieSessions { get; set; }

        public int? MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        public Movie? Movie { get; set; }
    }
}
