using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Services.Contracts;

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
    public async Task<ActionResult<Review>> GetReviewsForSpecificMovie(int movieid)
    {
        var review = await _reviewService.GetReviewsForSpecificMovieAsync(movieid);

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
