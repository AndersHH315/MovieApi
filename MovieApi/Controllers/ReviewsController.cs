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
    public async Task<ActionResult<IEnumerable<Review>>> GetReview()
    {
        var reviews = await _reviewService.GetReviewsAsync();

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
    public async Task<ActionResult<Review>> PostReview(int movieid, [FromQuery] ReviewDto reviewDto)
    {
        var review = await _reviewService.PostReviewAsync(movieid, reviewDto);

        return CreatedAtAction("GetReview", new ReviewDto(), review);
    }

    [HttpDelete("reviews/{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _reviewService.DeleteReviewAsync(id);

        return NoContent();
    }

}
