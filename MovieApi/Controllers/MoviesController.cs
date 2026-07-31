using Microsoft.AspNetCore.Mvc;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Services.Contracts;
using MovieApi.Core.Paging;


namespace MovieApi.Controllers;

[Route("api/")]
[ApiController]
public class MoviesController(IMovieService movieService) : ControllerBase
{
    private readonly IMovieService _movieService = movieService;

    [HttpGet("movies")]
    public async Task<ActionResult<PagedResult<MovieDto>>> GetMovies([FromQuery] PagingParameters paging)
    {
        var movies = await _movieService.GetAllMoviesAsync(paging);

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
    public async Task<IActionResult> PutMovie(int id, [FromQuery] MovieUpdateDto movieDto)
    {

        var movie = await _movieService.PutMovieAsync(id, movieDto);

        if (movie == null)
            return BadRequest();

        return Ok(movie);
    }

    [HttpPost("movies")]
    public async Task<IActionResult> PostMovie([FromBody]MovieCreateDto movieCreateDto)
    {
        var movie = await _movieService.PostMovieAsync(movieCreateDto);

        return CreatedAtAction("GetMovies", new MovieCreateDto(), movie);
    }

    [HttpDelete("movies/{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        await _movieService.DeleteMovieAsync(id);

        return NoContent();
    }
}
