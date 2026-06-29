using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Interfaces;
using MovieApi.Models;

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
    public async Task<ActionResult<ActorDto>> PutActor(int id, [FromQuery] ActorDto actorDto)
    {
        var actor = await _actorService.PutActorAsync(id, actorDto);

        if (actor == null)
            return BadRequest();

        return Ok(actor);
    }

    [HttpPost("actors")]
    public async Task<ActionResult<ActorDto>> PostActor([FromQuery] ActorDto actorDto)
    {
        var actor = await _actorService.PostActorAsync(actorDto);

        return CreatedAtAction("GetActor", new ActorDto(), actor);
    }

    [HttpPost("movies/{movieid}/actors/{actorid}")]
    public async Task<ActionResult<Actor>> AddActorToMovie(int actorid, int movieid)
    {
        var actorToMovie = await _actorService.AddActorToMovieAsync(actorid, movieid);

        if (actorToMovie == null)
            return NotFound();

        return Ok(actorToMovie);
    }

    [HttpDelete("actors/{id}")]
    public async Task<ActionResult<ActorDto>> DeleteActor(int id)
    {
        var actor = await _actorService.DeleteActorAsync(id);

        if (actor == null)
            return NotFound();

        return NoContent();
    }

}
