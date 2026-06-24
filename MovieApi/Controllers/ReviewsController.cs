using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Data;
using MovieApi.DTOs;

namespace MovieApi.Controllers;

[Route("api/")]
[ApiController]
public class ReviewsController(MovieApiContext context) : ControllerBase
{
    private readonly MovieApiContext _context = context;

    [HttpGet("reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview()
    {
        var reviews = await _context.Reviews.Select(r => new ReviewDto 
        {
            ReviewerName = r.ReviewerName,
            Comment = r.Comment,
            Rating = r.Rating
        }).ToListAsync();

        if (reviews == null)
            return NotFound();

        return Ok(reviews);
    }

    [HttpGet("movies/{movieid}/reviews")]
    public async Task<ActionResult<ReviewDto>> GetReviewsForSpecificMovie(int movieid)
    {
        var review = await _context.Reviews.Where(r => r.MovieId == movieid).Select(r => new ReviewDto
        {
            ReviewerName = r.ReviewerName,
            Comment = r.Comment,
            Rating = r.Rating
        }).ToListAsync();

        if (review == null)
            return NotFound();

        return Ok(review);
    }


    [HttpPost("movies/{movieid}/reviews")]
    public async Task<ActionResult<ReviewDto>> PostReview(int? movieid, [FromQuery] ReviewDto reviewDto)
    {
        var review = new Review
        {
            ReviewerName = reviewDto.ReviewerName,
            Comment = reviewDto.Comment,
            Rating = reviewDto.Rating,
            MovieId = (int)movieid
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetReview", new ReviewDto(), review);
    }

    [HttpDelete("reviews/{id}")]
    public async Task<IActionResult> DeleteReview(int? id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
