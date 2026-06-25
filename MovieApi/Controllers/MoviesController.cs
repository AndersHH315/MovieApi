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

        return Ok(movies);
    }

    [HttpGet("movies/{id}")]
    public async Task<ActionResult<Movie>> GetMovieById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);

        return Ok(movie);
    }

    [HttpGet("movies/{id}/details")]
    public async Task<ActionResult<IEnumerable<MovieDetailDto>>> GetMovieDetails(int id)
    {
        var movie = await _movieService.GetMovieDetailsAsync(id);
 
        return Ok(movie);
    }

    [HttpPut("movies/{id}")]
    public async Task<IActionResult> PutMovie(int id, [FromQuery] MovieUpdateDto movieDto)
    {

        var movie = await _movieService.PutMovieAsync(id, movieDto);

        return Ok();
    }

    [HttpPost("movies")]
    public async Task<ActionResult<MovieDto>> PostMovie([FromQuery]MovieDto movieDto)
    {
        var movie = await _movieService.PostMovieAsync(movieDto);

        return CreatedAtAction("GetMovies", new MovieDto(), movie);
    }

    [HttpDelete("movies/{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _movieService.DeleteMovieAsync(id);

        return Ok();
    }
}
