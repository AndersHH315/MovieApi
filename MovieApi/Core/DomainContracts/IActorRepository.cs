using MovieApi.Core.Models;

namespace MovieApi.Core.DomainContracts
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor?> GetActorAsync(int id);
        void Add(Actor actor);
        Task AddActorToMovie(int actorId, int movieId);
        void Update(Actor actor);
        void Remove(Actor actor);
    }
}
