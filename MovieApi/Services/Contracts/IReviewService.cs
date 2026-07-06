using Microsoft.AspNetCore.Mvc;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;

namespace MovieApi.Services.Contracts;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto?>?> GetReviewsAsync();
    Task<IEnumerable<ReviewDto>> GetReviewsForSpecificMovieAsync(int movieid);
    Task<ReviewDto?> PostReviewAsync(int movieid, ReviewDto reviewDto);
    Task<Review?> DeleteReviewAsync(int id);
}
