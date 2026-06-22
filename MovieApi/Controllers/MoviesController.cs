using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Data;
using MovieApi.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;

namespace MovieApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(MovieApiContext context) : ControllerBase
{
    private readonly MovieApiContext _context = context;

    // GET: api/Movie
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie()
    {
        return await _context.Movies.Select( m => new MovieDto
        {
            Title = m.Title,
            Year = m.Year,
            Duration = m.Duration,
            Genre = m.Genres.GenreType
        }).ToListAsync();
    }

    // GET: api/Movie/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _context.Movies.Where(m => m.Id == id).Select(m => new MovieDto { 
            Title = m.Title,
            Year = m.Year,
            Duration = m.Duration,
            Genre = m.Genres.GenreType
        }).FirstOrDefaultAsync();

        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    [HttpGet("{id}/details")]
    public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
    {
        var movie = await _context.Movies.Where(m => m.Id == id).Select(m => new MovieDetailDto
        {
            Title = m.Title,
            Year = m.Year,
            Duration = m.Duration,
            Genre = m.Genres.GenreType,
            Synopsis = m.MovieDetails.Synopsis,
            Language = m.MovieDetails.Language,
            Budget = m.MovieDetails.Budget,
            Reviews = m.Reviews.Select(r => new ReviewDto { ReviewerName = r.ReviewerName, Comment = r.Comment, Rating = r.Rating}).ToList(),
            Actors = m.Actors.Select(a => new ActorDto { Name = a.Name, BirthYear = a.BirthYear }).ToList()
        }).ToListAsync();
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    // PUT: api/Movie/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int? id, [FromQuery] MovieUpdateDto movieDto)
    {

        var movie = new Movie()
        {
            Id = _context.Movies.Where(m => m.Id == id).Select(m => m.Id).FirstOrDefault(),
            Title = movieDto.Title,
            Year = movieDto.Year,
            Duration = movieDto.Duration,
            GenreId = _context.Genres.Where(g => g.GenreType == movieDto.Genre).Select(g => g.Id).FirstOrDefault()
        };

        if (id != movie.Id)
        {
            return BadRequest();
        }

        _context.Entry(movie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id))
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

    // POST: api/Movie
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MovieDto>> PostMovie([FromQuery]MovieDto movieDto)
    {
        var movie = new Movie() {
            Title = movieDto.Title,
            Year = movieDto.Year,
            Duration = movieDto.Duration,
            GenreId = _context.Genres.Where(g => g.GenreType == movieDto.Genre).Select(g => g.Id).FirstOrDefault()
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new MovieDto(), movie);
    }

    // DELETE: api/Movie/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int? id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}
