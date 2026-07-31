using Microsoft.AspNetCore.Mvc;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;
using MovieApi.Services.Contracts;

namespace MovieApi.Controllers;

[Route("api/")]
[ApiController]
public class ReviewsController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;

    [HttpGet("reviews")]
    public async Task<ActionResult<PagedResult<ReviewDto>>> GetReviews([FromQuery] PagingParameters paging)
    {
        var reviews = await _reviewService.GetReviewsAsync(paging);

        if (reviews == null)
            return NotFound();

        return Ok(reviews);
    }

    [HttpGet("movies/{movieid}/reviews")]
    public async Task<ActionResult<PagedResult<ReviewDto>>> GetReviewsForSpecificMovie(int movieid, [FromQuery] PagingParameters paging)
    {
        var review = await _reviewService.GetReviewsForSpecificMovieAsync(movieid, paging);

        if (review == null)
            return NotFound();

        return Ok(review);
    }


    [HttpPost("movies/{movieid}/reviews")]
    public async Task<IActionResult> PostReview(int movieid, [FromBody] ReviewDto reviewDto)
    {
        var review = await _reviewService.PostReviewAsync(movieid, reviewDto);

        return CreatedAtAction("GetReviews", new ReviewDto(), review);
    }

    [HttpDelete("reviews/{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        await _reviewService.DeleteReviewAsync(id);

        return NoContent();
    }

}
