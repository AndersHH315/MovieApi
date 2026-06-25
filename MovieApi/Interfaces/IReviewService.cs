using Microsoft.AspNetCore.Mvc;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto?>?> GetReviewsAsync();
        Task<IEnumerable<ReviewDto>> GetReviewsForSpecificMovieAsync(int movieid);
        Task<Review> PostReviewAsync(int movieid, ReviewDto reviewDto);
        Task<Review?> DeleteReviewAsync(int id);
    }
}
