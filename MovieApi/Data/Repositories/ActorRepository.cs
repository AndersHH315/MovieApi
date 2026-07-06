using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DomainContracts;
using MovieApi.Core.Models;
using MovieApi.Services.Contracts;

namespace MovieApi.Data.Repositories;

public class ActorRepository(MovieApiContext db) : IActorRepository
{
    private readonly MovieApiContext _db = db;
    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        var actors = await _db.Actors.ToListAsync();
        return actors;
    }
    public async Task<Actor?> GetActorAsync(int id)
    {
        var actor = await _db.Actors.FindAsync(id);

        if (actor == null)
            return null;
        return actor;
    }
    public void Add(Actor actor)
    {
        _db.Actors.Add(actor);
    }

    public async Task AddActorToMovie(int actorId, int movieId)
    {
        var selectedMovie = await _db.Movies
        .Include(m => m.Actors)
        .FirstOrDefaultAsync(m => m.Id == movieId);
        var selectedActor = await _db.Actors.FirstOrDefaultAsync(a => a.Id == actorId);

        if (selectedMovie == null || selectedActor == null)
            throw new Exception("Movie or Actor not found");

        selectedMovie.Actors.Add(selectedActor);
    }
    public void Update(Actor actor)
    {
        _db.Actors.Update(actor);
    }
    public void Remove(Actor actor)
    {
        _db.Actors.Remove(actor);
    }
}

