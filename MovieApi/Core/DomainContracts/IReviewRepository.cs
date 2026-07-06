using MovieApi.Core.Models;

namespace MovieApi.Core.DomainContracts
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetReviewAsync(int id);
        Task<IEnumerable<Review>> GetReviewsByMovieId(int id);
        void Add(Review review);
        void Update(Review review);
        void Remove(Review review);
    }
}
