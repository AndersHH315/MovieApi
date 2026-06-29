using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.DTOs;
using MovieApi.Interfaces;
using MovieApi.Models;

namespace MovieApi.Services
{
    public class ActorService : IActorService
    {
        private readonly IMovieApiContext _db;
        public ActorService(IMovieApiContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ActorDto?>?> GetActorsAsync()
        {
            var actors = await _db.Actors.Select(a => new ActorDto
            {
                Name = a.Name,
                BirthYear = a.BirthYear
            }).ToListAsync();

            if (actors.Count == 0)
                return null;

            return actors;
        }

        public async Task<ActorDto?> GetActorByIdAsync(int id)
        {
            var actor = await _db.Actors.FindAsync(id);

            if (actor == null)
                return null;

            var actorDto = new ActorDto
            {
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };
            return actorDto;
        }

        public async Task<Actor?> PutActorAsync(int id, ActorDto actorDto)
        {
            var actor = await _db.Actors.FindAsync(id);

            if (actor == null)
                return null;

            actor.Name = actorDto.Name;
            actor.BirthYear = actorDto.BirthYear;

            _db.Actors.Entry(actor).State = EntityState.Modified;

            await _db.SaveChangesAsync();

            return actor;
        }

        public async Task<Actor> PostActorAsync(ActorDto actorDto)
        {
            var actor = new Actor()
            {
                Name = actorDto.Name,
                BirthYear = actorDto.BirthYear
            };
            _db.Actors.Add(actor);
            await _db.SaveChangesAsync();

            return actor;
        }

        public async Task<Actor?> AddActorToMovieAsync(int actorid, int movieid)
        {
            var selectedMovie = await _db.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == movieid);
            var selectedActor = await _db.Actors.FindAsync(actorid);

            if (selectedMovie == null || selectedActor == null)
                return null;

            selectedMovie.Actors.Add(selectedActor);
            await _db.SaveChangesAsync();
            return selectedActor;

        }

        public async Task<Actor?> DeleteActorAsync(int id)
        {
            var actor = await _db.Actors.FindAsync(id);
            if (actor == null)
                return null;
      
            _db.Actors.Remove(actor);
            await _db.SaveChangesAsync();

            return actor;
        }
    }
}
