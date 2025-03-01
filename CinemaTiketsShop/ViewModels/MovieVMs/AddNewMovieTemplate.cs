using CinemaTiketsShop.ViewModels.CinemaVMs;

namespace CinemaTiketsShop.ViewModels.MovieVMs
{
    public class AddNewMovieTemplate : AbstractAddNewTemplate
    {
        public new required string Name { get; set; }

        public new required string Description { get; set; }

        public new required string Logo { get; set; }

        public new required double Price { get; set; }
    }
}
