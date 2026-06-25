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

        return Ok(actors);
    }

    [HttpGet("actors/{id}")]
    public async Task<ActionResult<ActorDto>> GetActor(int id)
    {
        var actor = await _actorService.GetActorAsync(id);

        return Ok(actor);
    }

 
    [HttpPut("actors/{id}")]
    public async Task<IActionResult> PutActor(int id, [FromQuery] ActorDto actorDto)
    {
        var actor = await _actorService.PutActorAsync(id, actorDto);

        return Ok();
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

        return Ok();
    }

    [HttpDelete("actors/{id}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        var actor = await _actorService.DeleteActorAsync(id);

        return Ok();
    }

}
