using MovieApi.Core.DomainContracts;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Services.Contracts;
using MovieApi.Core.Paging;

namespace MovieApi.Services;

public class ActorService(IUnitOfWork unit) : IActorService
{
    private readonly IUnitOfWork _unit = unit;

    public async Task<PagedResult<ActorDto>> GetActorsAsync(PagingParameters paging)
    {
        var actors = await _unit.Actors.GetAllAsync();
        return await actors.Select(a => new ActorDto
        {
            Name = a.Name,  
            BirthYear = a.BirthYear
        }).AsQueryable().ToPagedResult(paging.CurrentPage, paging.PageSize);
    }

    public async Task<ActorDto?> GetActorByIdAsync(int id)
    {
        var actor = await _unit.Actors.GetActorAsync(id);

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
        var actor = await _unit.Actors.GetActorAsync(id);

        if (actor == null)
            return null;

        actor.Name = actorDto.Name;
        actor.BirthYear = actorDto.BirthYear;

        _unit.Actors.Update(actor);
        await _unit.SaveAsync();

        return actor;
    }

    public async Task<Actor> PostActorAsync(ActorDto actorDto)
    {
        var actor = new Actor()
        {
            Name = actorDto.Name,
            BirthYear = actorDto.BirthYear
        };
        _unit.Actors.Add(actor);
        await _unit.SaveAsync();

        return actor;
    }

    public async Task AddActorToMovieAsync(int actorid, int movieid)
    {
        await _unit.Actors.AddActorToMovie(actorid, movieid);
        await _unit.SaveAsync();

    }

    public async Task<Actor?> DeleteActorAsync(int id)
    {
        var actor = await _unit.Actors.GetActorAsync(id);
        if (actor == null)
            return null;
  
        _unit.Actors.Remove(actor);
        await _unit.SaveAsync();

        return actor;
    }
}
