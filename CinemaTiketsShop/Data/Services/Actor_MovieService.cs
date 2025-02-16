using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Services
{
    public class Actor_MovieService : IActor_MovieService
    {
        public readonly ApplicationDbConntext _context;

        public Actor_MovieService(ApplicationDbConntext context)
        {
            _context = context;
        }

        public async Task AddActorsToMovie(int MovieId, List<int> ActorsId)
        {
            try
            {
                List<Movie_Actor> MovieActors = new List<Movie_Actor>();

                foreach (var actor in ActorsId)
                {
                    MovieActors.Add(new Movie_Actor { MovieId = MovieId, ActorId = actor });
                }

                await _context.Movies_Actors.AddRangeAsync(MovieActors);

                await _context.SaveChangesAsync();
            }
            catch (OperationCanceledException ex)
            {
                throw new OperationCanceledException("Create MovieActor canceled",ex);
            }
            catch 
            {
                throw new Exception("Unexpected error by creating MovieActor");
            }
        }
    }
}
