using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTiketsShop.Models
{
    public class CinemaHall_MovieSession
    {
        public int CinemaHallId { get; set; }
        [ForeignKey("CinemaHallId")]
        public required CinemaHall CinemaHall { get; set; }

        public int MovieSessionId { get; set; }
        [ForeignKey("MovieSessionId")]
        public required MovieSession MovieSession { get; set; }
    }
}
