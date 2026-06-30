using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Interfaces;

namespace MovieApi.Controllers;

[Route("api/")]
[ApiController]
public class ReviewsController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;

    [HttpGet("reviews")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
        var reviews = await _reviewService.GetReviewsAsync();

        if (reviews == null)
            return NotFound();

        return Ok(reviews);
    }

    [HttpGet("movies/{movieid}/reviews")]
    public async Task<ActionResult<ReviewDto>> GetReviewsForSpecificMovie(int movieid)
    {
        var review = await _reviewService.GetReviewsForSpecificMovieAsync(movieid);

        if (review == null)
            return NotFound();

        var reviewDto = review.Select(r => new ReviewDto
        {
            ReviewerName = r.ReviewerName,
            Comment = r.Comment,
            Rating = r.Rating
        });

        return Ok(reviewDto);
    }


    [HttpPost("movies/{movieid}/reviews")]
    public async Task<ActionResult<ReviewDto>> PostReview(int movieid, [FromQuery] ReviewDto reviewDto)
    {
        var review = await _reviewService.PostReviewAsync(movieid, reviewDto);

        return CreatedAtAction("GetReviews", new ReviewDto(), review);
    }

    [HttpDelete("reviews/{id}")]
    public async Task<ActionResult<ReviewDto>> DeleteReview(int id)
    {
        var review = await _reviewService.DeleteReviewAsync(id);

        if (review == null)
            return NotFound();

        return NoContent();
    }

}
