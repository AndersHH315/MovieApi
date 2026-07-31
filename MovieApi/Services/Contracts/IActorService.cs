using Microsoft.AspNetCore.Mvc;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;

namespace MovieApi.Services.Contracts;

public interface IActorService
{
    Task<PagedResult<ActorDto>> GetActorsAsync(PagingParameters paging);
    Task<ActorDto?> GetActorByIdAsync(int id);
    Task<Actor?> PutActorAsync(int id, ActorDto actorDto);
    Task<Actor> PostActorAsync(ActorDto actorDto);
    Task AddActorToMovieAsync(int actorid, int movieid);
    Task<Actor?> DeleteActorAsync(int id);
}
 
