using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Data;
using MovieApi.DTOs;

namespace MovieApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController(MovieApiContext context) : ControllerBase
{
    private readonly MovieApiContext _context = context;

    // GET: api/Review
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReview()
    {
        return await _context.Reviews.ToListAsync();
    }

    [HttpGet("{movieid}/movies")]
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
    // PUT: api/Review/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int? id, Review review)
    {
        if (id != review.Id)
        {
            return BadRequest();
        }

        _context.Entry(review).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReviewExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    //POST: api/Review
    //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("{movieid}")]
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

    // DELETE: api/Review/5
    [HttpDelete("{id}")]
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

    private bool ReviewExists(int? id)
    {
        return _context.Reviews.Any(e => e.Id == id);
    }
}
