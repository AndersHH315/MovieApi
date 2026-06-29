using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Interfaces;
using MovieApi.Models;


namespace MovieApi.Controllers;

[Route("api/")]
[ApiController]
public class MoviesController(IMovieService movieService) : ControllerBase
{
    private readonly IMovieService _movieService = movieService;

    [HttpGet("movies")]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();

        if (movies == null)
            return NotFound();

        return Ok(movies);
    }

    [HttpGet("movies/{id}")]
    public async Task<ActionResult<Movie>> GetMovieById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    [HttpGet("movies/{id}/details")]
    public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
    {
        var movie = await _movieService.GetMovieDetailsAsync(id);

        if (movie == null)
            return NotFound();
 
        return Ok(movie);
    }

    [HttpPut("movies/{id}")]
    public async Task<ActionResult<MovieDto>> PutMovie(int id, [FromQuery] MovieUpdateDto movieDto)
    {

        var movie = await _movieService.PutMovieAsync(id, movieDto);

        if (movie == null)
            return BadRequest();

        return Ok(movie);
    }

    [HttpPost("movies")]
    public async Task<ActionResult<MovieDto>> PostMovie([FromQuery]MovieDto movieDto)
    {
        var movie = await _movieService.PostMovieAsync(movieDto);

        return CreatedAtAction("GetMovies", new MovieDto(), movie);
    }

    [HttpDelete("movies/{id}")]
    public async Task<ActionResult<MovieDto>> DeleteMovie(int id)
    {
        var movie = await _movieService.DeleteMovieAsync(id);

        if (movie == null)
            return NotFound();

        return NoContent();
    }
}
