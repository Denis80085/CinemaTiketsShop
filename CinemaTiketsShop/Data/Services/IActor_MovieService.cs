namespace CinemaTiketsShop.Data.Services
{
    public interface IActor_MovieService
    {
        Task AddActorsToMovie(int MovieId, List<int> ActorsId);
    }
}
