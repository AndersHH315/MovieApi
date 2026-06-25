using Microsoft.AspNetCore.Mvc;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDto?>?> GetActorsAsync();
        Task<ActorDto?> GetActorAsync(int id);
        Task<Actor?> PutActorAsync(int id, ActorDto actorDto);
        Task<Actor> PostActorAsync(ActorDto actorDto);
        Task<Actor?> AddActorToMovieAsync(int actorid, int movieid);
        Task<Actor?> DeleteActorAsync(int id);
    }
     
}
