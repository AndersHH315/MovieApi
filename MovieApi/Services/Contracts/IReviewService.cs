using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;

namespace MovieApi.Services.Contracts;

public interface IReviewService
{
    Task<PagedResult<ReviewDto>> GetReviewsAsync(PagingParameters paging);
    Task<IEnumerable<ReviewDto>> GetReviewsForSpecificMovieAsync(int movieid);
    Task<ReviewDto?> PostReviewAsync(int movieid, ReviewDto reviewDto);
    Task<Review?> DeleteReviewAsync(int id);
}
