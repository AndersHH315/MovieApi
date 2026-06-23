using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Data;
using MovieApi.DTOs;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace MovieApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ActorsController(MovieApiContext context) : ControllerBase
{
    private readonly MovieApiContext _context = context;

    // GET: api/Actor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActor()
    {

        return await _context.Actors.Select(a => new ActorDto 
        {
            Name = a.Name,
            BirthYear = a.BirthYear
        }).ToListAsync();
    }

    // GET: api/Actor/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ActorDto>> GetActor(int id)
    {
        var actor = await _context.Actors.Where(a => a.Id == id).Select(a => new ActorDto
        {
            Name = a.Name,
            BirthYear = a.BirthYear
        }).FirstOrDefaultAsync();

        if (actor == null)
        {
            return NotFound();
        }

        return Ok(actor);
    }

    // PUT: api/Actor/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActor(int? id, [FromQuery] ActorDto actorDto)
    {
        var actor = new Actor()
        {
            Id = _context.Actors.Where(a => a.Id == id).Select(a => a.Id).FirstOrDefault(),
            Name = actorDto.Name,
            BirthYear = actorDto.BirthYear
        };

        if (id != actor.Id)
        {
            return BadRequest();
        }

        _context.Entry(actor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActorExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok();
    }

    // POST: api/Actor
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ActorDto>> PostActor([FromQuery] ActorDto actorDto)
    {
        var actor = new Actor()
        {
            Name = actorDto.Name,
            BirthYear = actorDto.BirthYear
        };
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetActor", new ActorDto(), actor);
    }

    [HttpPost("{actorId}/movies/{movieId}")]
    public async Task<ActionResult<Actor>> AddActorToMovie(int actorId, int movieId)
    {
        var selectedMovie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == movieId);
        var selectedActor = await _context.Actors.FindAsync(actorId);

        if (selectedMovie == null || selectedActor == null)
            return NotFound();

        selectedMovie.Actors.Add(selectedActor);
        await _context.SaveChangesAsync();
        return Ok();
    }

    // DELETE: api/Actor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(int? id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor == null)
        {
            return NotFound();
        }

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private bool ActorExists(int? id)
    {
        return _context.Actors.Any(e => e.Id == id);
    }
}
