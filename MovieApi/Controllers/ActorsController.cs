using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Data;
using MovieApi.Services.Contracts;

namespace MovieApi.Controllers;
[Route("api/")]
[ApiController]
public class ActorsController(IActorService actorService) : ControllerBase
{
    private readonly IActorService _actorService = actorService;

    [HttpGet("actors")]
    public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
    {
        var actors = await _actorService.GetActorsAsync();

        if (actors == null)
            return NotFound();

        return Ok(actors);
    }

    [HttpGet("actors/{id}")]
    public async Task<ActionResult<ActorDto>> GetActorById(int id)
    {
        var actor = await _actorService.GetActorByIdAsync(id);

        if (actor == null)
            return NotFound();

        return Ok(actor);
    }

 
    [HttpPut("actors/{id}")]
    public async Task<IActionResult> PutActor(int id, [FromQuery] ActorDto actorDto)
    {
        var actor = await _actorService.PutActorAsync(id, actorDto);

        if (actor == null)
            return BadRequest();

        return Ok(actor);
    }

    [HttpPost("actors")]
    public async Task<IActionResult> PostActor([FromBody] ActorDto actorDto)
    {
        var actor = await _actorService.PostActorAsync(actorDto);

        return CreatedAtAction("GetActors", new ActorDto(), actor);
    }

    [HttpPost("movies/{movieid}/actors/{actorid}")]
    public async Task<IActionResult> AddActorToMovie(int actorid, int movieid)
    {
        await _actorService.AddActorToMovieAsync(actorid, movieid);

        return NoContent();
    }

    [HttpDelete("actors/{id}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        await _actorService.DeleteActorAsync(id);

        return NoContent();
    }

}
