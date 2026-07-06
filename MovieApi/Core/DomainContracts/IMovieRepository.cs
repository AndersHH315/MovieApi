using MovieApi.Core.Models;

namespace MovieApi.Core.DomainContracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetMovieAsync(int id);
        Task<Movie?> GetMovieDetailsById(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Remove(Movie movie);
    }
}
